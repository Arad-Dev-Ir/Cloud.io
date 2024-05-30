namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Cloud.Core.Models;
using EventId = Contract.EventId;

public class EventModelValidator : EventPipeline
{
    private readonly ILogger<EventModelValidator> _logger;

    public EventModelValidator(IServiceProvider serviceProvider, ILogger<EventModelValidator> logger) : base(serviceProvider)
    => _logger = logger;

    public override async Task ExecuteAsync<E>(E @event)
    {
        var eventId = EventId.EventValidationException;
        var eventType = @event.GetType();
        var time = DateTime.Now;
        try
        {
            await Next.ExecuteAsync(@event);
        }
        catch (AppException e)
        {
            _logger.LogError(eventId, e,
            "Processing of {EventType} with value {Event} failed at {StartDateTime} because there are domain exceptions.",
            eventType,
            @event,
            time);
        }
        catch (AggregateException e)
        {
            if (e.InnerException is AppException domainStateException)
            {
                _logger.LogError(eventId,
                domainStateException,
                "Processing of {EventType} with value {Event} failed at {StartDateTime} because there are domain exceptions.",
                eventType,
                @event,
                time);
            }
            else
                throw e;
        }
    }
}