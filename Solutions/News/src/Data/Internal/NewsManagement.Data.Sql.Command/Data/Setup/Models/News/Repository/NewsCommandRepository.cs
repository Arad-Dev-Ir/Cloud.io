namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.News.Contracts;
using Sql.Commands;
using News = Core.News.Models.News;

public class NewsCommandRepository : CommandRepository<NewsManagementCommandContext, News>, INewsCommandRepository
{
    public NewsCommandRepository(NewsManagementCommandContext context) : base(context)
    { }
}