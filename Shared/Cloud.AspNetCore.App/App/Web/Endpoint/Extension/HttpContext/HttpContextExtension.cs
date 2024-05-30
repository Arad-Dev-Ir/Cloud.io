namespace Cloud.Web.Endpoint.API;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Web.Core.AppService;

public static partial class HttpContextExtension
{
    public static CommandPipeline GetCommandDispatcher(this HttpContext source)
    => (CommandPipeline)source.RequestServices.GetRequiredService(typeof(CommandPipeline));

    public static QueryPipeline GetQueryDispatcher(this HttpContext source)
    => (QueryPipeline)source.RequestServices.GetRequiredService(typeof(QueryPipeline));

    public static EventPipeline GetEventDispatcher(this HttpContext source)
    => (EventPipeline)source.RequestServices.GetRequiredService(typeof(EventPipeline));
}