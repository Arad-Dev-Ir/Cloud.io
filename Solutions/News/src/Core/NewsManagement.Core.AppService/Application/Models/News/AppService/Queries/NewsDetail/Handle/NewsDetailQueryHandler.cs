namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class NewsDetailQueryHandler : QueryHandler<NewsDetail, NewsDetailResult>
{
    private readonly INewsQueryRepository _repo;

    public NewsDetailQueryHandler(INewsQueryRepository repo)
    => _repo = repo;

    public override async Task<QueryResponse<NewsDetailResult>> ExecuteAsync(NewsDetail query, CancellationToken cancellationToken)
    {
        var data = await _repo.Query(query, cancellationToken);
        var result = await SetAsync(data);
        return result;
    }
}