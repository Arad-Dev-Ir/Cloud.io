namespace Cloudio.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public abstract class Interceptor : SaveChangesInterceptor
{
    protected abstract Task OnSave(DbContext context, CancellationToken token);

    protected static DbContext GetContext(DbContextEventData eventData)
    => eventData.Context!;
}