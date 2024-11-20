namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.Keyword.Contracts;
using KeywordsManagement.Core.Keyword.Models;


public sealed class ChangeKeywordTitleCommandHandler(IKeywordCommandRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<ChangeKeywordTitleCommand>
{
    private readonly IKeywordCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(ChangeKeywordTitleCommand input, CancellationToken token)
    {
        var id = Id.CreateInstance(input.Id);
        var keywordTitle = KeywordTitle.CreateInstance(input.Title);

        var keyword = await _repository.GetGraphAsync(id, token);

        if (keyword is null)
            throw new KeywordNotfoundException(id.Value);

        if (await _repository.ExistsAsync(e => e.Title == keywordTitle && e.Id != id, token))
            throw new KeywordAlreadyExistsException(keywordTitle.Value);

        keyword.ChangeTitle(input.Title);
        await _unitOfWork.SaveAsync(token);
    }
}