namespace Cloud.Web.Core.AppService;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cloud.Core;
using Web.Core.Contract;
using EventId = Contract.EventId;

public class QueryRequestValidator : QueryPipeline
{
    public QueryRequestValidator(IServiceProvider serviceProvider, ILogger<QueryRequestValidator> logger) : base(serviceProvider)
    => _logger = logger;

    private readonly ILogger<QueryRequestValidator> _logger;

    private const int _eventId = EventId.QueryValidationException;
    public override async Task<QueryResponse<D>> ExecuteAsync<Q, D>(Q query)
    {
        var result = default(QueryResponse<D>);

        var queryType = query.GetType();
        var time = DateTime.Now;

        _logger.LogDebug(_eventId,
        "Validating query of type {QueryType} with value {Query} started at :{StartDateTime}",
        queryType,
        query,
        time);

        var validationResult = Validate<Q, QueryResponse<D>>(query);
        if (validationResult.Status == ServiceStatus.Ok || validationResult.Status == ServiceStatus.NoService)
        {
            _logger.LogDebug(_eventId,
            "Validating query of type {QueryType} with value {Query} finished at :{EndDateTime}",
            queryType,
            query,
            time);

            result = await Next.ExecuteAsync<Q, D>(query);
        }
        else
        {
            result = validationResult;

            _logger.LogInformation(_eventId,
            "Validating query of type {QueryType} with value {Query} failed. Validation errors are: {ValidationErrors}",
            queryType,
            query,
            validationResult.Messages);
        }
        return result;
    }

    private R Validate<Q, R>(Q query) where Q : IQuery where R : AppServiceResponse, new()
    {
        var result = default(R);

        var validator = ServiceProvider.GetService<IValidator<Q>>();
        var type = query.GetType();

        validator.IsNotNull(
        () =>
        {
            var validationResult = validator.Validate(query);
            var isValid = validationResult.IsValid;
            if (isValid) result = new() { Status = ServiceStatus.Ok, };
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

            _logger.LogInformation(_eventId,
            "There is not any validator for {QueryType}",
            type);
        });
        return result;
    }
}
