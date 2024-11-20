namespace Cloudio.Web.Core.AppService;

using System;
using FluentValidation;
using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public class RequestValidation(IServiceProvider serviceProvider) : AbstractRequestController(serviceProvider)
{
    public override async Task PublishAsync<TInput>(TInput input, CancellationToken token)
    {
        var validationResult = await Validate(input);
        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);

        await Next!.PublishAsync<TInput>(input, token);
    }

    public override async Task<TOutput> PublishAsync<TInput, TOutput>(TInput input, CancellationToken token)
    {
        var validationResult = await Validate(input);
        if (!validationResult.IsValid)
            throw new AppValidationException(validationResult.Errors);

        var result = await Next!.PublishAsync<TInput, TOutput>(input, token);
        return result;
    }

    #region Validation

    private async Task<ValidationResult> Validate<TInput>(TInput input) where TInput : IRequest
    {
        var result = new Dictionary<string, object?>();
        var validator = ServiceProvider.GetService<IValidator<TInput>>();
        if (validator is { })
        {
            var validationResult = await validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                var failures = validationResult.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage, (key, messages)
                    => new
                    {
                        Property = key,
                        Messages = messages
                    }
                )
                .ToList();

                result = new Dictionary<string, object?>()
                {
                    {"failures", failures }
                };
            }
        }
        return new ValidationResult { Errors = result };
    }

    #endregion
}