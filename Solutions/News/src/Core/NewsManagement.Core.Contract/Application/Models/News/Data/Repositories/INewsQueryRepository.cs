namespace NewsManagement.Core.News.Contracts;

using Cloudio.Web.Core.Contract;

public interface INewsQueryRepository : IQueryRepository
{
    Task<NewsDetailQueryResponse> Query(NewsDetailQuery query, CancellationToken token);

    Task<PagedData<NewsListQueryResponse>> Query(NewsListQuery query, CancellationToken token);
}