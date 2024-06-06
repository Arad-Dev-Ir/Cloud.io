namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Web.Core.Contract;
using EventId = Contract.EventId;

public class QueryDispatcher : QueryPipeline
{
    public QueryDispatcher(IServiceProvider serviceProvider, ILogger<QueryDispatcher> logger) : base(serviceProvider)
    {
        _logger = logger;
        _stopwatch = new();
    }

    private readonly ILogger<QueryDispatcher> _logger;
    private readonly Stopwatch _stopwatch;

    private const int _eventId = EventId.PerformanceMeasurement;
    public override async Task<QueryResponse<T>> ExecuteAsync<Q, T>(Q query, CancellationToken cancellationToken)
    {
        _stopwatch.Start();

        var queryType = query.GetType();
        var time = DateTime.Now;

        try
        {
            var queryHandler = ServiceProvider.GetRequiredService<IQueryHandler<Q, T>>();
            return await queryHandler.ExecuteAsync(query, cancellationToken);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e,
            "There is not suitable handler for {QueryType} routing failed at {StartDateTime}.",
            queryType,
            time);
            throw;
        }
        finally
        {
            _stopwatch.Stop();

            _logger.LogInformation(_eventId,
            "Processing the {QueryType} query took {Millisecconds} millisecconds",
            queryType,
            _stopwatch.ElapsedMilliseconds);
        }
    }
}
