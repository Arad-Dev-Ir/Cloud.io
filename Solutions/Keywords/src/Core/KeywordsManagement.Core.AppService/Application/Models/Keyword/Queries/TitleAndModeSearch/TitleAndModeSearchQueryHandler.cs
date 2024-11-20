namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.Keyword.Contracts;

public sealed class TitleAndModeSearchQueryHandler(IKeywordQueryRepository repository)
    : IRequestHandler<TitleAndStateSearchQuery, PagedData<TitleAndStateSearchQueryResponse>>
{
    private readonly IKeywordQueryRepository _repository = repository;

    public async Task<PagedData<TitleAndStateSearchQueryResponse>> HandleAsync(TitleAndStateSearchQuery input, CancellationToken token)
    {
        var result = await _repository.Query(input, token);
        return result;
    }
}