namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Cloud.Core.Extensions.Identity;
using System.Threading;
using System.Threading.Tasks;

public class AuditDataInterceptor : Interceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        OnSave(GetContext(eventData));
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        OnSave(GetContext(eventData));
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    protected override void OnSave(DbContext context)
    => SetAuditData(context);

    private static void SetAuditData(DbContext context)
    => context.ChangeTracker.SetAuditData(context.GetService<IUserIdentity>());
}