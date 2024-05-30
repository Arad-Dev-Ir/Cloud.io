namespace Cloud.Web.Endpoint.API;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using Cloud.Core.Models;
using Cloud.Core.Extensions.Serialization;

public class ApiErrorMiddleware : Model
{
    public ApiErrorMiddleware(ApiErrorOption option, RequestDelegate next, IJsonSerializer serializer, ILogger<ApiErrorMiddleware> logger)
    {
        _next = next;
        _option = option;
        _serializer = serializer;
        _logger = logger;
    }
    private readonly ApiErrorOption _option;
    private readonly RequestDelegate _next;
    private readonly IJsonSerializer _serializer;
    private readonly ILogger<ApiErrorMiddleware> _logger;

    public async Task Invoke(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception e) { await HandleApiErrorAsync(context, e); }
    }

    private async Task HandleApiErrorAsync(HttpContext context, Exception exception)
    {
        var internalServerErrorStatus = HttpStatusCode.InternalServerError;
        var status = (short)internalServerErrorStatus;
        var error = new ApiError
        {
            Id = Guid.NewGuid().ToString(),
            Status = status,
            Title = "SOME_KIND_OF_ERROR_OCCURRED_IN_THE_API"
        };

        _option.SetResponeDetail?.Invoke(context, exception, error);
        var logLevel = _option.SetLogLevel?.Invoke(exception) ?? LogLevel.Error;

        var innerError = GetInnerExeptionMessage(exception);
        _logger.Log(logLevel, exception, "BADNESS!!! " + innerError + " -- {ErrorId}.", error.Id);

        var content = _serializer.Serialize(error);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = status;
        await context.Response.WriteAsync(content);
    }

    private string GetInnerExeptionMessage(Exception exception)
    => exception.InnerException != null ? GetInnerExeptionMessage(exception.InnerException) : exception.Message;
}