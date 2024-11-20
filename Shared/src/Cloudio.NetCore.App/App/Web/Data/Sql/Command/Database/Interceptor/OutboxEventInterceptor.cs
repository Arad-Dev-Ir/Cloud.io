namespace Cloudio.Web.Data.Sql.Command;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Cloudio.Core.Services.Identity;
using Cloudio.Core.Services.Serialization;

public class OutboxEventInterceptor : ShadowPropertyInterceptor
{
    protected override async Task OnSave(DbContext context, CancellationToken token)
    {
        await base.OnSave(context, token);
        SaveOutboxEvents(context);
    }

    #region Outbox Events

    private static void SaveOutboxEvents(DbContext context)
    {
        var changeTracker = context.ChangeTracker;
        var modules = changeTracker.GetModulesWithEvents();
        var userInfo = context.GetService<IUserIdentity>();
        var serializer = context.GetService<IJsonSerializer>();

        foreach (var item in modules)
        {
            var moduleType = item.GetType();
            var events = item.Events;
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                var result = new OutboxEvent
                {
                    UserId = userInfo.GetUserId(),
                    Name = eventType.Name,
                    Type = eventType.FullName!,
                    Payload = serializer.Serialize(@event),
                    State = EventProcessingState.Raised,
                    ModuleCode = item.Code.Value.ToString(),
                    ModuleName = moduleType.Name,
                    ModuleType = moduleType.FullName!
                };
                context.Add<OutboxEvent>(result);
            }
            item.ClearEvents();
        }
    }

    #endregion
}