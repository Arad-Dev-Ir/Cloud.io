namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.Keyword.Contracts;

public sealed class ActivateKeywordCommandHandler(IKeywordCommandRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<ActivateKeywordCommand>
{
    private readonly IKeywordCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(ActivateKeywordCommand input, CancellationToken token)
    {
        var id = input.Id;
        var keyword = await _repository.GetGraphAsync(id, token);

        if (keyword is null)
            throw new KeywordNotfoundException(id);

        keyword.Activate();
        await _unitOfWork.SaveAsync(token);
    }
}