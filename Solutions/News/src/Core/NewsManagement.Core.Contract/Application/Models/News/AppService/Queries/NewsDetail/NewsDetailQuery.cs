namespace NewsManagement.Core.News.Contracts;

using Cloudio.Web.Core.Contract;

public sealed record NewsDetailQuery : IRequest<NewsDetailQueryResponse>
{
    public long Id { get; init; }
}