namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Diagnostics;
using Web.Core.Contract;

public class EventDispatcher : EventPipeline
{
    public EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger) : base(serviceProvider)
    {
        _logger = logger;
        _stopwatch = new();
    }

    private readonly ILogger<EventDispatcher> _logger;
    private readonly Stopwatch _stopwatch;

    public override async Task ExecuteAsync<T>(T @event)
    {
        _stopwatch.Start();
        var eventType = @event.GetType();
        var eventHandlersCount = 0;
        var time = DateTime.Now;
        var counter = 0;

        try
        {
            _logger.LogDebug("Routing event of type {Type} with value {Event} started at {Time}",
            eventType,
            @event,
            time);
            var eventHandlers = ServiceProvider.GetServices<IEventHandler<T>>();
            eventHandlersCount = eventHandlers.Count();
            var tasks = new List<Task>();
            foreach (var item in eventHandlers)
            {
                tasks.Add(item.ExecuteAsync(@event));
                counter++;
            }
            await Task.WhenAll(tasks);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e,
            "There is not suitable handler for {EventType}, routing failed at {StartDateTime}.",
            eventType,
            time);

            throw;
        }
        finally
        {
            _stopwatch.Stop();
            _logger.LogDebug("Total number of handlers for {EventType} is {Count} and handled events number is {Counter}. The process took {Millisecconds} millisecconds",
            eventType,
            eventHandlersCount,
            counter,
            _stopwatch.ElapsedMilliseconds);
        }
    }
}
