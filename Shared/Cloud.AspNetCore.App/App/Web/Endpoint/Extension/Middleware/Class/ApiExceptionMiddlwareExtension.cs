namespace Cloud.Web.Endpoint.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Logging;

    public static partial class ApiExceptionMiddlwareExtension
    {
        public static void UseApiException(this IApplicationBuilder source)
        {
            source.UseApiErrorMiddleware(e =>
            {
                e.SetResponeDetail = (context, exception, error) =>
                {
                    if (exception.GetType().Name == typeof(SqlException).Name)
                        error.Detail = "Exception was a database exception!";
                };
                e.SetLogLevel = (exception) =>
                {
                    var result = LogLevel.Error;
                    var comparisonMode = StringComparison.InvariantCultureIgnoreCase;
                    var condition = exception.Message.StartsWith("cannot open database", comparisonMode) || exception.Message.StartsWith("a network-related", comparisonMode);
                    if (condition)
                        result = LogLevel.Critical;
                    return result;
                };
            });
        }

        internal static IApplicationBuilder UseApiErrorMiddleware(this IApplicationBuilder source)
        {
            var option = new ApiErrorOption();
            source.UseMiddleware<ApiErrorMiddleware>(option);
            return source;
        }
        internal static IApplicationBuilder UseApiErrorMiddleware(this IApplicationBuilder source, Action<ApiErrorOption> act)
        {
            var option = new ApiErrorOption();
            act(option);
            source.UseMiddleware<ApiErrorMiddleware>(option);
            return source;
        }
    }
}