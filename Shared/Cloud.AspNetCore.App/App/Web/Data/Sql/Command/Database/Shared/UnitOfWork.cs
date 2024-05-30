namespace Cloud.Web.Data.Sql.Command;

using Web.Core.Contract;

public class UnitOfWork<C> : IUnitOfWork, IDisposable, IAsyncDisposable where C : CommandContext
{
    private readonly C _context;

    public UnitOfWork(C context)
    => _context = context;

    public async Task PerformTransactionalAsync(Action act)
    {
        try
        {
            await StartAsync();
            act();
            await CommitAsync();
            await DisposeAsync();
        }
        catch (Exception)
        {
            await RollbackAsync();
            await DisposeAsync();
            throw;
        }
    }

    #region operations

    public async Task StartAsync()
    => await _context.StartAsync();

    public async Task CommitAsync()
    => await _context.CommitAsync();

    public async Task RollbackAsync()
    => await _context.RollbackAsync();

    public async Task SaveAsync()
    => await _context.SaveAsync();

    public void Dispose()
    => _context.Dispose();

    public async ValueTask DisposeAsync()
    => await _context.DisposeAsync();

    #endregion
}