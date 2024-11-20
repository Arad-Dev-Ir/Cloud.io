namespace NewsManagement.Data.Sql.Queries;

using Microsoft.EntityFrameworkCore;
using Cloudio.Web.Data.Sql.Query;
using NewsManagement.Data.Sql.News.Queries;

public sealed class NewsManagementQueryContext(DbContextOptions<NewsManagementQueryContext> options)
    : QueryContext(options)
{
    public DbSet<News> News => Set<News>();
    public DbSet<NewsKeyword> NewsKeywords => Set<NewsKeyword>();
}