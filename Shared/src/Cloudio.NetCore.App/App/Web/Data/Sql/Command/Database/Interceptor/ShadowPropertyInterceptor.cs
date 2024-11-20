namespace Cloudio.Web.Data.Sql.Command;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Cloudio.Core.Services.Identity;
using Cloudio.Web.Core.AppService;

public class ShadowPropertyInterceptor : Interceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> interceptionResult, CancellationToken token = default)
    {
        var dbcontext = GetContext(eventData);
        await OnSave(dbcontext, token);

        var result = await base.SavingChangesAsync(eventData, interceptionResult, token);
        return result;
    }

    protected override async Task OnSave(DbContext context, CancellationToken token)
    {
        await PublishEvents(context, token);
        await SetEntitiesShadowPropertyValues(context);
    }

    #region Publish Events and Set Shadow Properties' Values

    private static async Task PublishEvents(DbContext context, CancellationToken token)
    {
        var eventController = context.GetService<IEventController>();

        var modules = context.ChangeTracker.GetModulesWithEvents();
        var events = modules
        .SelectMany(e => e.Events)
        .ToList();

        foreach (var item in events)
        {
            await eventController.PublishAsync((dynamic)item, token);
        }

        await Task.CompletedTask;
    }

    private static async Task SetEntitiesShadowPropertyValues(DbContext context)
    {
        context.ChangeTracker.SetEntitiesShadowPropertyValues(context.GetService<IUserIdentity>());
        await Task.CompletedTask;
    }

    #endregion
}