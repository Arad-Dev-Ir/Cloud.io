namespace Cloudio.Web.Data.Sql.Command;

using Cloudio.Web.Core.Contract;

public class UnitOfWork<C>(C context) : IUnitOfWork, IDisposable, IAsyncDisposable where C : CommandContext
{
    private readonly C _context = context;

    public async Task StartAsync(CancellationToken token)
    => await _context.StartAsync(token);

    public async Task CommitAsync(CancellationToken token)
    => await _context.CommitAsync(token);

    public async Task RollbackAsync(CancellationToken token)
    => await _context.RollbackAsync(token);

    public async Task SaveAsync(CancellationToken token)
    => await _context.SaveAsync(token);

    #region Dispose

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_context is { })
                _context.Dispose();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (_context is { })
                await _context.DisposeAsync();
        }
    }

    #endregion
}