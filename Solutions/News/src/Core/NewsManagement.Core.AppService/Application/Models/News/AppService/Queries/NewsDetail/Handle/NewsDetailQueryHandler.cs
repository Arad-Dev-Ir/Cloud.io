namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public sealed class NewsDetailQueryHandler(INewsQueryRepository repository) : QueryHandler<NewsDetail, NewsDetailResult>
{
    private readonly INewsQueryRepository _repository = repository;

    public override async Task<QueryResponse<NewsDetailResult>> ExecuteAsync(NewsDetail query, CancellationToken cancellationToken)
    {
        var data = await _repository.Query(query, cancellationToken);
        var result = await SetAsync(data);
        return result;
    }
}