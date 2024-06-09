namespace NewsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Command;
using System.Reflection;
using Core.News.Models;

public class NewsManagementCommandContext(DbContextOptions<NewsManagementCommandContext> options) : OutboxCommandContext(options)
{
    public DbSet<News> News => Set<News>();

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(SetConfigurations(builder));

    private static ModelBuilder SetConfigurations(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}