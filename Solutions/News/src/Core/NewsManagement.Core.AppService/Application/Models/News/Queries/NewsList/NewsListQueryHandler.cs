namespace NewsManagement.Core.News.AppServices;

using Cloudio.Web.Core.Contract;
using NewsManagement.Core.News.Contracts;

public sealed class NewsListQueryHandler(INewsQueryRepository repository) : IRequestHandler<NewsListQuery, PagedData<NewsListQueryResponse>>
{
    private readonly INewsQueryRepository _repository = repository;

    public async Task<PagedData<NewsListQueryResponse>> HandleAsync(NewsListQuery input, CancellationToken token)
    {
        var result = await _repository.Query(input, token);
        return result;
    }
}