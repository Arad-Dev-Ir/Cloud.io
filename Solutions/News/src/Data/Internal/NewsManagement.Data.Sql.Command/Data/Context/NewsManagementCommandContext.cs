namespace NewsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Command;
using System.Reflection;
using News = Core.News.Models.News;


public class NewsManagementCommandContext : OutboxCommandContext
{
    public DbSet<News> News => Set<News>();

    public NewsManagementCommandContext(DbContextOptions<NewsManagementCommandContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(SetConfigurations(builder));

    private ModelBuilder SetConfigurations(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}