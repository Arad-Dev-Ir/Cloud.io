namespace Cloud.Web.Core.AppService;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cloud.Core;
using Web.Core.Contract;
using EventId = Web.Core.Contract.EventId;

public class CommandRequestValidator : CommandPipeline
{
    private readonly ILogger<CommandRequestValidator> _logger;

    public CommandRequestValidator(IServiceProvider serviceProvider, ILogger<CommandRequestValidator> logger) : base(serviceProvider)
    => _logger = logger;

    private const int _eventId = EventId.CommandValidationException;
    public override async Task<CommandResponse> ExecuteAsync<C>(C command, CancellationToken cancellationToken)
    {
        var result = default(CommandResponse);
        var type = command.GetType();
        var time = DateTime.Now;

        _logger.LogDebug(_eventId,
        "Validating command of type {CommandType} with value {Command} started at: {Time}",
        type,
        command,
        time);

        var validationResult = ValidateCommand<C, CommandResponse>(command);
        if (validationResult.Status == ServiceStatus.Ok || validationResult.Status == ServiceStatus.NoService)
        {
            _logger.LogDebug(_eventId,
            "Validating command of type {CommandType} With value {Command} finished at :{Time}",
            type,
            command,
            time);

            result = await Next.ExecuteAsync<C>(command, cancellationToken);
        }
        else
        {
            _logger.LogInformation(_eventId,
            "Validating command of type {CommandType} With value {Command}  failed. Validation errors are: {ValidationErrors}",
            type,
            command,
            validationResult.Messages);

            result = validationResult;
        }
        return result;
    }

    public override async Task<CommandResponse<T>> ExecuteAsync<C, T>(C command, CancellationToken cancellationToken)
    {
        var result = default(CommandResponse<T>);
        var type = command.GetType();
        var time = DateTime.Now;
        _logger.LogDebug(_eventId,
        "Validating command of type {CommandType} with value {Command} started at: {Time}",
        type,
        command,
        time);

        var validationResult = ValidateCommand<C, CommandResponse<T>>(command);
        var status = validationResult.Status == ServiceStatus.Ok || validationResult.Status == ServiceStatus.NoService;
        if (status)
        {
            _logger.LogDebug(_eventId,
            "Validating command of type {CommandType} with value {Command} finished at: {Time}",
            type,
            command,
            time);

            result = await Next.ExecuteAsync<C, T>(command, cancellationToken);
        }
        else
        {
            _logger.LogInformation(_eventId,
            "Validating command of type {CommandType} with value {Command} failed. Validation errors are: {ValidationErrors}",
            type,
            command,
            validationResult.Messages);

            result = validationResult;
        }
        return result;
    }

    #region Private Methods

    private R ValidateCommand<C, R>(C command) where C : ICommand where R : AppServiceResponse, new()
    {
        var result = default(R);
        var validator = ServiceProvider.GetService<IValidator<C>>();
        var type = command.GetType();
        if (validator is not null)
        {
            var validationResult = validator.Validate(command);
            var isValid = validationResult.IsValid;
            if (isValid) result = new() { Status = ServiceStatus.Ok };
            else
            {
                result = new() { Status = ServiceStatus.ValidationError };
                var errors = validationResult.Errors;
                errors.ForEach(e => result.AddMessage(e.ErrorMessage));
            }
        }
        else
        {
            result = new() { Status = ServiceStatus.NoService };

            _logger.LogInformation(_eventId,
            "There is not any validator for {CommandType}",
            type);
        }
        return result;
    }

    #endregion
}