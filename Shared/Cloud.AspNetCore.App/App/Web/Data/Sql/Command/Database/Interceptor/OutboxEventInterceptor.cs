namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Core.Extensions.Identity;
using Cloud.Core.Extensions.Serialization;

public class OutboxEventInterceptor : AuditDataInterceptor
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
    {
        base.OnSave(context);
        SetOutboxEventData(context);
    }

    private static void SetOutboxEventData(DbContext context)
    {
        var changeTracker = context.ChangeTracker;
        var modules = changeTracker.GetModulesWithEvent();
        var userInfo = context.GetService<IUserIdentity>();
        var serializer = context.GetService<IJsonSerializer>();
        var date = DateTime.Now;

        foreach (var item in modules)
        {
            var moduleType = item.GetType();
            var events = item.GetEvents();
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                var result = new OutboxEvent
                {
                    UserId = userInfo.GetUserId(),
                    Date = date,
                    Name = eventType.Name,
                    Type = eventType.FullName,
                    Data = serializer.Serialize(@event),
                    Mode = ProcessMode.Raised,
                    ModuleId = item.Code.ToString(),
                    ModuleName = moduleType.Name,
                    ModuleType = moduleType.FullName
                };
                context.Add<OutboxEvent>(result);
            }
            item.ClearEvents();
        }
    }
}
