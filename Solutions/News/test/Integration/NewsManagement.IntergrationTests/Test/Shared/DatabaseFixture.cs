namespace NewsManagement.Test.Integrations;

using Microsoft.EntityFrameworkCore;

public abstract class DatabaseFixture<TDbContext> : IDisposable where TDbContext : DbContext
{
    public async Task<TDbContext> BuildDbContext(string dbName)
    {
        try
        {
            var options = new DbContextOptionsBuilder<TDbContext>()
                        .UseInMemoryDatabase(dbName)
                        .EnableSensitiveDataLogging()
                        .Options;

            var context = BuildDbContext(options);
            context.Database.EnsureCreated();
            await Seed();

            return context;
        }
        catch (Exception ex)
        {
            throw new Exception($"unable to connect to db", ex);
        }
    }

    protected abstract TDbContext BuildDbContext(DbContextOptions<TDbContext> options);
    protected abstract Task Seed();

    public virtual void Dispose()
    => GC.SuppressFinalize(this);
}