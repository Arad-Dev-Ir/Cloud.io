namespace NewsManagement.Core.News.AppServices;

using Cloudio.Web.Core.Contract;
using NewsManagement.Core.News.Contracts;

public sealed class NewsDetailQueryHandler(INewsQueryRepository repository) : IRequestHandler<NewsDetailQuery, NewsDetailQueryResponse>
{
    private readonly INewsQueryRepository _repository = repository;

    public async Task<NewsDetailQueryResponse> HandleAsync(NewsDetailQuery input, CancellationToken token)
    {
        var result = await _repository.Query(input, token);
        return result;
    }
}