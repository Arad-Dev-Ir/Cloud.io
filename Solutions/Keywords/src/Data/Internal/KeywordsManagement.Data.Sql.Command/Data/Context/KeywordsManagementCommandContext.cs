namespace KeywordsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Command;
using System.Reflection;
using Keyword = Core.Keyword.Models.Keyword;
using NewsService = Core.NewsService.Models.NewsService;

public sealed class KeywordsManagementCommandContext(DbContextOptions<KeywordsManagementCommandContext> options) : OutboxCommandContext(options)
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<NewsService> NewsServices => Set<NewsService>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => base.OnConfiguring(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(SetConfigurations(builder));

    private static ModelBuilder SetConfigurations(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}