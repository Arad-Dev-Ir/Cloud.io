namespace Cloudio.Web.Endpoint.API;

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Cloudio.Core;
using Cloudio.Core.Models;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    #region Constants

    private const string RequestProblemTitle = "An error occurred while executing the request";

    private const string ServerProblemTitle = "An error occurred in the server.";
    private const string ServerProblemDetail = "An error occurred in the server. For more information, please see the log file";

    #endregion

    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken token)
    {
        var errorDetails = GetProblemDetailFromException(exception);
        httpContext.Response.StatusCode = errorDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(errorDetails, token);

        return true;
    }

    #region Private Methods

    private ErrorDetails GetProblemDetailFromException(Exception exception)
    {
        var id = UniqueIdentifier.GetIdAsString();
        var result = exception switch
        {
            AppDomainException ex => new ErrorDetails
            {
                Id = id,
                Title = RequestProblemTitle,
                Status = StatusCodes.Status400BadRequest,
                Detail = $"{ex.Message}.",
                Extensions = ex.Metadata ?? default!
            },
            _ => new ErrorDetails
            {
                Id = id,
                Title = ServerProblemTitle,
                Status = StatusCodes.Status500InternalServerError,
                Detail = $"{ServerProblemDetail}."
            }
        };

        RegisterLog(exception, result);
        return result;
    }

    private static string GetInnerExeptionMessage(Exception exception)
    => exception.InnerException != null ? GetInnerExeptionMessage(exception.InnerException) : exception.Message;

    private void RegisterLog(Exception exception, ErrorDetails errorDetails)
    {
        var innerExceptionMessage = GetInnerExeptionMessage(exception);
        var message = innerExceptionMessage.EndsWith('.') ? innerExceptionMessage.TrimEnd('.') : innerExceptionMessage;

        var comparisonMode = StringComparison.InvariantCultureIgnoreCase;
        var isCriticalException = exception.Message.StartsWith("cannot open database", comparisonMode)
            || exception.Message.StartsWith("a network-related", comparisonMode);

        var loglevel = isCriticalException ? LogLevel.Critical : LogLevel.Error;
        _logger.Log(loglevel, exception, "An error occurred in the server with Id '{0}'. {1}.", errorDetails.Id, message);
    }

    #endregion
}