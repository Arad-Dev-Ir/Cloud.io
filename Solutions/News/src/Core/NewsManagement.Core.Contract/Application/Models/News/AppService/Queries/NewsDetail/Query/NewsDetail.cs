namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public class NewsDetail : IQuery<NewsDetailResult>
{
    public long Id { get; set; }
}