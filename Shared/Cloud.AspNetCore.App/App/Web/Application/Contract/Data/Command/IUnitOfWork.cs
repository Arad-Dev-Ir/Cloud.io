namespace Cloud.Web.Core.Contract;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task PerformTransactionalAsync(Action act);
    Task StartAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveAsync();
}