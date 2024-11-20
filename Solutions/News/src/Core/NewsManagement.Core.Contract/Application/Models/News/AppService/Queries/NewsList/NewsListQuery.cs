namespace NewsManagement.Core.News.Contracts;

using Cloudio.Web.Core.Contract;

public sealed record NewsListQuery : PageQuery<PagedData<NewsListQueryResponse>>;