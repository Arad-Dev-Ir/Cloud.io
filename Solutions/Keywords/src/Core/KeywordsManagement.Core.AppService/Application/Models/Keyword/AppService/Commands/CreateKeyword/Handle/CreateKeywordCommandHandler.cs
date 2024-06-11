namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using Models;

public sealed class CreateKeywordCommandHandler(IKeywordCommandRepository repository, IUnitOfWork unitOfWork) : CommandHandler<CreateKeyword, long>
{
    private readonly IKeywordCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public override async Task<CommandResponse<long>> ExecuteAsync(CreateKeyword command, CancellationToken cancellationToken)
    {
        var keyword = Keyword.Instance(command.Title);
        _repository.Add(keyword);
        await _unitOfWork.SaveAsync(cancellationToken);

        var result = await OkAsync(keyword.Id.Value);
        return result;
    }
}