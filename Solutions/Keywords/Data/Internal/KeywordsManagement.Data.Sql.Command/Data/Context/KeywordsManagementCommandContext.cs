namespace KeywordsManagement.Data.Sql.Commands;

using Microsoft.EntityFrameworkCore;
using Cloud.Web.Data.Sql.Command;
using System.Reflection;
using Keyword = Core.Keyword.Models.Keyword;
using NewsService = Core.NewsService.Models.NewsService;

public class KeywordsManagementCommandContext : OutboxCommandContext
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<NewsService> NewsServices => Set<NewsService>();

    public KeywordsManagementCommandContext(DbContextOptions<KeywordsManagementCommandContext> options) : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(SetConfigurations(builder));

    private ModelBuilder SetConfigurations(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}