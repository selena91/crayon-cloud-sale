using Microsoft.EntityFrameworkCore;

namespace CrayonCloudSale.Core.BaseUoW;

public class BaseUoW<TDbContext> : IBaseUoW
    where TDbContext : DbContext
{
    private bool _disposedValue;

    public BaseUoW(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    protected TDbContext? DbContext { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext?.SaveChangesAsync(cancellationToken)!;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            DbContext?.Dispose();
        }

        _disposedValue = true;
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (DbContext != null)
        {
            await DbContext.DisposeAsync().ConfigureAwait(false);
        }
    }
}
