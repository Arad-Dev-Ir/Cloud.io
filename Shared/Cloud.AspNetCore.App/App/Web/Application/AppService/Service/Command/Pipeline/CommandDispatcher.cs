namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Core.Contract;
using EventId = Web.Core.Contract.EventId;


public class CommandDispatcher : CommandPipeline
{
    public CommandDispatcher(IServiceProvider serviceProvider, ILogger<CommandDispatcher> logger) : base(serviceProvider)
    {
        _logger = logger;
        _stopwatch = new();
    }

    private readonly ILogger<CommandDispatcher> _logger;
    private readonly Stopwatch _stopwatch;

    private const int _eventId = EventId.PerformanceMeasurement;
    public override async Task<CommandResponse> ExecuteAsync<C>(C command)
    {
        var result = default(CommandResponse);
        _stopwatch.Start();

        var commandType = command.GetType();
        var time = DateTime.Now;

        try
        {
            _logger.LogDebug("Routing command of type {CommandType} with value {Command} started at {StartDateTime}",
            commandType,
            command,
            time);

            var commandHandler = ServiceProvider.GetRequiredService<ICommandHandler<C>>();
            result = await commandHandler.ExecuteAsync(command);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is not suitable handler for {CommandType} routing failed at {StartDateTime}.",
            commandType,
            time);

            throw;
        }
        finally
        {
            _stopwatch.Stop();

            _logger.LogInformation(_eventId,
            "Processing the {CommandType} command tooks {Millisecconds} millisecconds",
            commandType,
            _stopwatch.ElapsedMilliseconds);
        }
        return result;
    }

    public override async Task<CommandResponse<D>> ExecuteAsync<C, D>(C command)
    {
        var result = default(CommandResponse<D>);

        var commandType = command.GetType();
        var time = DateTime.Now;

        _stopwatch.Start();
        try
        {
            _logger.LogDebug("Routing command of type {CommandType} with value {Command} started at {StartDateTime}",
            command.GetType(),
            command,
            time);

            var commandHandler = ServiceProvider.GetRequiredService<ICommandHandler<C, D>>();
            result = await commandHandler.ExecuteAsync(command);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "There is not suitable handler for {CommandType} routing failed at {StartDateTime}.",
            commandType,
            time);

            throw;
        }
        finally
        {
            _stopwatch.Stop();

            _logger.LogInformation(_eventId,
            "Processing the {CommandType} command took {Millisecconds} millisecconds",
            commandType,
            _stopwatch.ElapsedMilliseconds);
        }
        return result;
    }
}