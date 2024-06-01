namespace NewsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Query;
using NewsManagement.Data.Sql.News.Queries;

public class NewsManagementQueryContext(DbContextOptions<NewsManagementQueryContext> options) : QueryContext(options)
{
    public DbSet<News> News => Set<News>();
    public DbSet<NewsKeyword> NewsKeywords => Set<NewsKeyword>();
}