namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class NewsRecordsQueryHandler : QueryHandler<NewsRecords, PagedData<NewsRecordsResult>>
{
    private readonly INewsQueryRepository _repo;

    public NewsRecordsQueryHandler(INewsQueryRepository repo)
    => _repo = repo;

    public override async Task<QueryResponse<PagedData<NewsRecordsResult>>> ExecuteAsync(NewsRecords query, CancellationToken cancellationToken)
    {
        var data = await _repo.Query(query, cancellationToken);
        data.Page = query.Page;
        data.PageSize = query.PageSize;

        var result = await SetAsync(data);
        return result;
    }
}