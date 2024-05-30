namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentValidation;
using System.Threading.Tasks;
using Cloud.Core;
using Cloud.Core.Models;
using Web.Core.Contract;
using EventId = Contract.EventId;

public class EventRequestValidator : EventPipeline
{
    private readonly ILogger<EventRequestValidator> _logger;

    public EventRequestValidator(IServiceProvider serviceProvider, ILogger<EventRequestValidator> logger) : base(serviceProvider)
    => _logger = logger;

    public override async Task ExecuteAsync<E>(E @event)
    {
        var eventId = EventId.EventValidationException;
        var eventType = @event.GetType();
        var time = DateTime.Now;

        _logger.LogDebug(eventId, "Validating Event of type {EventType} with value {Event} started at :{StartDateTime}", eventType, @event, time);

        var validationResult = ValidateEvent<E, EventResponse>(@event);
        var status = validationResult.Status == ServiceStatus.Ok || validationResult.Status == ServiceStatus.NoService;
        if (status)
        {
            _logger.LogDebug(eventId, "Validating query of type {QueryType} with value {Query} finished at :{EndDateTime}", eventType, @event, time);
            await Next.ExecuteAsync(@event);
        }
        else
            _logger.LogInformation(eventId,
            "Validating query of type {QueryType} with value {Query} failed. Validation errors are: {ValidationErrors}",
            eventType, @event, validationResult.Messages);
    }


    private R ValidateEvent<E, R>(E @event) where E : Model, IEvent where R : AppServiceResponse, new()
    {
        var result = default(R);

        var validator = ServiceProvider.GetService<IValidator<E>>();
        var eventType = @event.GetType();
        validator.IsNotNull(
        () =>
        {
            var validationResult = validator.Validate(@event);
            var isValid = validationResult.IsValid;
            if (isValid) result = new() { Status = ServiceStatus.Ok };
            else
            {
                result = new() { Status = ServiceStatus.ValidationError };
                var errors = validationResult.Errors;
                errors.ForEach(e => result.AddMessage(e.ErrorMessage));
            }
        },
        () =>
        {
            result = new() { Status = ServiceStatus.NoService };

            _logger.LogInformation(EventId.CommandValidationException,
            "There is not any validator for {EventType}",
            eventType);
        });
        return result;
    }
}