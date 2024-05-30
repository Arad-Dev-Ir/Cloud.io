namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public abstract class Interceptor : SaveChangesInterceptor
{
    protected abstract void OnSave(DbContext context);

    protected DbContext GetContext(DbContextEventData eventData)
    => eventData.Context;
}