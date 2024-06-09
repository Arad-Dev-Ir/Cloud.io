namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public sealed class NewsDetailQueryHandler(INewsQueryRepository repo) : QueryHandler<NewsDetail, NewsDetailResult>
{
    private readonly INewsQueryRepository _repo = repo;

    public override async Task<QueryResponse<NewsDetailResult>> ExecuteAsync(NewsDetail query, CancellationToken cancellationToken)
    {
        var data = await _repo.Query(query, cancellationToken);
        var result = await SetAsync(data);
        return result;
    }
}