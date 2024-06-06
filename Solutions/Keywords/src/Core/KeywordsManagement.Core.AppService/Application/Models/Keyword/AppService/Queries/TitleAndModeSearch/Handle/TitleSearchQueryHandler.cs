namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class TitleSearchQueryHandler : QueryHandler<TitleAndModeSearch, PagedData<TitleAndModeSearchResult>>
{
    private readonly IKeywordQueryRepository _repo;

    public TitleSearchQueryHandler(IKeywordQueryRepository repo)
    => _repo = repo;

    public override async Task<QueryResponse<PagedData<TitleAndModeSearchResult>>> ExecuteAsync(TitleAndModeSearch query, CancellationToken cancellationToken)
    {
        var data = await _repo.Query(query, cancellationToken);
        data.Page = query.Page;
        data.PageSize = query.PageSize;

        var result = await SetAsync(data);
        return result;
    }
}