namespace Cloud.Web.Core.Contract;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task PerformTransactionalAsync(Action act, CancellationToken cancellationToken);
    Task StartAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}