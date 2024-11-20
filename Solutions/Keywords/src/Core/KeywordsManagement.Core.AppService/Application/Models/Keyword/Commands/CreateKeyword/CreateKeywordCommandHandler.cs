namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.Keyword.Contracts;
using KeywordsManagement.Core.Keyword.Models;

public sealed class CreateKeywordCommandHandler(IKeywordCommandRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateKeywordCommand, CreateKeywordCommandResponse>
{
    private readonly IKeywordCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateKeywordCommandResponse> HandleAsync(CreateKeywordCommand input, CancellationToken token)
    {
        var title = input.Title;
        var keywordTitle = KeywordTitle.CreateInstance(title);

        if (await _repository.ExistsAsync(e => e.Title == keywordTitle, token))
            throw new KeywordAlreadyExistsException(keywordTitle.Value);

        var keyword = Keyword.CreateInstance(title);
        _repository.Add(keyword);
        await _unitOfWork.SaveAsync(token);

        var result = new CreateKeywordCommandResponse(keyword.Id.Value);
        return result;
    }
}