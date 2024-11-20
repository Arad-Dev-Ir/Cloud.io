namespace Cloudio.Web.Data.Sql.Query;

using Microsoft.EntityFrameworkCore;

public class QueryContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    => base.OnModelCreating(builder);

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}