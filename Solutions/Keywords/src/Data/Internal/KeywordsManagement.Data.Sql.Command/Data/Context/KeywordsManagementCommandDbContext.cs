namespace KeywordsManagement.Data.Sql.Commands;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.Keyword.Models;
using KeywordsManagement.Core.NewsService.Models;

public sealed class KeywordsManagementCommandDbContext(DbContextOptions<KeywordsManagementCommandDbContext> options)
    : OutboxCommandContext(options)
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<NewsService> NewsServices => Set<NewsService>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    => base.OnConfiguring(builder);

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(SetConfigurations(builder));

    private static ModelBuilder SetConfigurations(ModelBuilder builder)
    => builder
        .HasDefaultSchema(KeywordsManagementCommandDbContextSchema.DefaultSchema)
        .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}