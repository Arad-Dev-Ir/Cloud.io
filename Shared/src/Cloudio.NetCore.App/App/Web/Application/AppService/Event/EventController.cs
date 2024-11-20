namespace Cloudio.Web.Core.AppService;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public class EventController(IServiceProvider serviceProvider) : IEventController
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken token) where TEvent : IEvent
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        List<Task> tasks = [];
        foreach (var item in handlers)
        {
            tasks.Add(item.HandleAsync(@event, token));
        }
        await Task.WhenAll(tasks);
    }
}