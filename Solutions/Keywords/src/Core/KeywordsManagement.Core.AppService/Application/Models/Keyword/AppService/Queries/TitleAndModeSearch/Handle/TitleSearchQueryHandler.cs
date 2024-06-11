namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public sealed class TitleSearchQueryHandler(IKeywordQueryRepository repository) : QueryHandler<TitleAndModeSearch, PagedData<TitleAndModeSearchResult>>
{
    private readonly IKeywordQueryRepository _repository = repository;

    public override async Task<QueryResponse<PagedData<TitleAndModeSearchResult>>> ExecuteAsync(TitleAndModeSearch query, CancellationToken cancellationToken)
    {
        var data = await _repository.Query(query, cancellationToken);
        var result = await SetAsync(data);
        return result;
    }
}