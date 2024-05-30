namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;

public class OutboxCommandContext : CommandContext
{
    public DbSet<OutboxEvent> Events => Set<OutboxEvent>();

    public OutboxCommandContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new OutboxEventConfiguration());
    }
}