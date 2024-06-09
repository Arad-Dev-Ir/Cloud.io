namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public sealed record NewsDetail : IQuery<NewsDetailResult>
{
    public long Id { get; init; }
}