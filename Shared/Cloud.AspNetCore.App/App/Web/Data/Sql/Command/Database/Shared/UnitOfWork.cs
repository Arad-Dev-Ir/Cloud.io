namespace Cloud.Web.Data.Sql.Command;

using Web.Core.Contract;

public class UnitOfWork<C> : IUnitOfWork, IDisposable, IAsyncDisposable where C : CommandContext
{
    private readonly C _context;

    public UnitOfWork(C context)
    => _context = context;

    public async Task PerformTransactionalAsync(Action act, CancellationToken cancellationToken)
    {
        try
        {
            await StartAsync(cancellationToken);
            act();
            await CommitAsync(cancellationToken);
            await DisposeAsync();
        }
        catch (Exception)
        {
            await RollbackAsync(cancellationToken);
            await DisposeAsync();
            throw;
        }
    }

    #region operations

    public async Task StartAsync(CancellationToken cancellationToken)
    => await _context.StartAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken)
    => await _context.CommitAsync(cancellationToken);

    public async Task RollbackAsync(CancellationToken cancellationToken)
    => await _context.RollbackAsync(cancellationToken);

    public async Task SaveAsync(CancellationToken cancellationToken)
    => await _context.SaveAsync(cancellationToken);

    public void Dispose()
    => _context.Dispose();

    public async ValueTask DisposeAsync()
    => await _context.DisposeAsync();

    #endregion
}