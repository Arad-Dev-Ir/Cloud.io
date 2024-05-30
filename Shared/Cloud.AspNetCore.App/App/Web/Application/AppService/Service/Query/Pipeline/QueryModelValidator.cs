namespace Cloud.Web.Core.AppService;

using Microsoft.Extensions.Logging;
using Cloud.Core.Models;
using Web.Core.Contract;
using EventId = Web.Core.Contract.EventId;

public class QueryModelValidator : QueryPipeline
{
    public QueryModelValidator(IServiceProvider serviceProvider, ILogger<QueryModelValidator> logger) : base(serviceProvider)
    => _logger = logger;

    private readonly ILogger<QueryModelValidator> _logger;

    private const int _eventId = EventId.DomainValidationException;
    public override async Task<QueryResponse<D>> ExecuteAsync<Q, D>(Q query)
    {
        var result = default(QueryResponse<D>);

        var queryType = query.GetType();
        var time = DateTime.Now;

        try
        {
            result = await Next.ExecuteAsync<Q, D>(query);
        }
        // for sync mode.
        catch (AppException e)
        {
            _logger.LogError(_eventId,
            e,
            "Processing of {QueryType} with value {Query} failed at {StartDateTime} because there are domain exceptions.",
            queryType,
            query,
            time);

            result = SetResponse<Q, D>(e);
        }
        // for async mode.
        catch (AggregateException e)
        {
            if (e.InnerException is AppException domainStateException)
            {
                _logger.LogError(_eventId,
                domainStateException,
                "Processing of {QueryType} with value {Query} failed at {StartDateTime} because there are domain exceptions.",
                queryType,
                query,
                time);

                result = SetResponse<Q, D>(domainStateException);
            }
            else throw e;
        }
        return result;
    }

    private QueryResponse<D> SetResponse<Q, D>(AppException exception)
    {
        var result = new QueryResponse<D>()
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
        "Domain exception message is {DomainExceptionMessage}",
        result);
        return result;
    }
}