namespace Cloudio.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;

public class OutboxCommandContext(DbContextOptions options) : CommandContext(options)
{
    public DbSet<OutboxEvent> Events => Set<OutboxEvent>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new OutboxEventConfiguration());
    }
}