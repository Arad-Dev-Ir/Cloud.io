namespace Cloud.Web.Data.Sql.Query;

using Microsoft.EntityFrameworkCore;

public class QueryContext : DbContext
{
    public QueryContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(builder);

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    public override int SaveChanges()
   => throw new NotSupportedException();

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    => throw new NotSupportedException();

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    => throw new NotSupportedException();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => throw new NotSupportedException();
}
