namespace Cloudio.Web.Core.Contract;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task SaveAsync(CancellationToken token);

    Task StartAsync(CancellationToken token);

    Task CommitAsync(CancellationToken token);

    Task RollbackAsync(CancellationToken token);
}