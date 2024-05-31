namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using Keyword = Models.Keyword;

public class CreateKeywordCommandHandler : CommandHandler<CreateKeyword, long>
{
    private readonly IKeywordCommandRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateKeywordCommandHandler(IKeywordCommandRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    private void Initialize()
    { }

    public override async Task<CommandResponse<long>> ExecuteAsync(CreateKeyword command)
    {
        var result = default(CommandResponse<long>);
        var keyword = Keyword.Instance(command.Title);
        _repo.Add(keyword);
        await _unitOfWork.SaveAsync();
        result = await OkAsync(keyword.Id.Value);
        return result;
    }
}
