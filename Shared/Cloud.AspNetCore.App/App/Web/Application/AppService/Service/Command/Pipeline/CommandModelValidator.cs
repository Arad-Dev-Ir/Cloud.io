namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.Logging;
using Cloud.Core.Models;
using Web.Core.Contract;
using EventId = Web.Core.Contract.EventId;

public class CommandModelValidator : CommandPipeline
{
    private readonly ILogger<CommandModelValidator> _logger;

    public CommandModelValidator(IServiceProvider serviceProvider, ILogger<CommandModelValidator> logger) : base(serviceProvider)
    => _logger = logger;


    private const int _eventId = EventId.DomainValidationException;
    public override async Task<CommandResponse> ExecuteAsync<C>(C command)
    {
        var result = default(CommandResponse);

        var commandType = command.GetType();
        var time = DateTime.Now;

        try
        {
            result = await Next.ExecuteAsync<C>(command);
        }
        catch (AppException e)
        {
            _logger.LogError(_eventId,
            e,
            "Processing of {CommandType} with value {Command} failed at {StartDateTime} because there are domain exceptions.",
            commandType,
            command,
            time);
            result = SetResponse<C>(e);
        }
        catch (AggregateException e)
        {
            if (e.InnerException is AppException domainStateException)
            {
                _logger.LogError(_eventId,
                domainStateException,
                "Processing of {CommandType} with value {Command} failed at {StartDateTime} because there are domain exceptions.",
                commandType,
                command,
                time);

                result = SetResponse<C>(domainStateException);
            }
            else throw e;
        }
        return result;
    }

    public override async Task<CommandResponse<D>> ExecuteAsync<C, D>(C command)
    {
        var result = default(CommandResponse<D>);

        var commandType = command.GetType();
        var time = DateTime.Now;

        try
        {
            result = await Next.ExecuteAsync<C, D>(command);
        }
        // for sync mode.
        catch (AppException e)
        {
            _logger.LogError(_eventId,
            e,
            "Processing of {CommandType} with value {Command} failed at {StartDateTime} because there are domain exceptions.",
            commandType,
            command,
            time);

            result = SetResponse<C, D>(e);
        }
        // for async mode.
        catch (AggregateException e)
        {
            if (e.InnerException is AppException domainStateException)
            {
                _logger.LogError(_eventId,
                domainStateException,
                "Processing of {CommandType} with value {Command} failed at {StartDateTime} because there are domain exceptions.",
                commandType,
                command,
                time);

                result = SetResponse<C, D>(domainStateException);
            }
            else throw e;
        }
        return result;
    }

    #region Private Methods

    private CommandResponse SetResponse<C>(AppException exception)
    {
        var result = new CommandResponse()
        {
            Status = ServiceStatus.InvalidDomainState
        };
        var exceptionText = GetExceptionText(exception);
        result.AddMessage(exceptionText);
        return result;
    }

    private CommandResponse<D> SetResponse<C, D>(AppException exception)
    {
        var result = new CommandResponse<D>()
        {
            Status = ServiceStatus.InvalidDomainState
        };
        var exceptionText = GetExceptionText(exception);
        result.AddMessage(exceptionText);
        return result;
    }

    private string GetExceptionText(AppException exception)
    {
        var result = exception.ToString();
        _logger.LogInformation(_eventId,
        "Domain exception message is: {DomainExceptionMessage}",
        result);
        return result;
    }

    #endregion
}


