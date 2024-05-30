namespace Cloud.Web.Endpoint.API;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Cloud.Core.Models;

public class ApiErrorOption : Model
{
    public Action<HttpContext, Exception, ApiError> SetResponeDetail { get; set; } = (context, exception, apiError) => { };
    public Func<Exception, LogLevel> SetLogLevel { get; set; } = (exception) => LogLevel.Error;
}
