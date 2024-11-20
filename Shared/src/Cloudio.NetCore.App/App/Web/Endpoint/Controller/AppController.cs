namespace Cloudio.Web.Endpoint.API;

using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Cloudio.Web.Core.AppService;
using Cloudio.Web.Core.Contract;

[ApiController]
public class AppController : Controller
{
    protected IRequestController RequestController => HttpContext.GetRequestController();

    protected async Task<IActionResult> CreateAsync<TInput, TOutput>(TInput request, CancellationToken token) where TInput : IRequest<TOutput>
    {
        var result = StatusCode((int)HttpStatusCode.Created, await RequestController.PublishAsync<TInput, TOutput>(request, token));
        return result;
    }

    protected async Task<IActionResult> CreateAsync<TInput>(TInput request, CancellationToken token) where TInput : IRequest
    {
        await RequestController.PublishAsync<TInput>(request, token);

        var result = StatusCode((int)HttpStatusCode.Created);
        return result;
    }


    protected async Task<IActionResult> EditAsync<TInput, TOutput>(TInput request, CancellationToken token) where TInput : IRequest<TOutput>
    {
        var result = StatusCode((int)HttpStatusCode.OK, await RequestController.PublishAsync<TInput, TOutput>(request, token));
        return result;
    }

    protected async Task<IActionResult> EditAsync<TInput>(TInput request, CancellationToken token) where TInput : IRequest
    {
        await RequestController.PublishAsync<TInput>(request, token);

        var result = StatusCode((int)HttpStatusCode.OK);
        return result;
    }


    protected async Task<IActionResult> DeleteAsync<TInput, TOutput>(TInput request, CancellationToken token) where TInput : IRequest<TOutput>
    {
        var result = StatusCode((int)HttpStatusCode.NoContent, await RequestController.PublishAsync<TInput, TOutput>(request, token));
        return result;
    }

    protected async Task<IActionResult> DeleteAsync<TInput>(TInput request, CancellationToken token) where TInput : IRequest
    {
        await RequestController.PublishAsync<TInput>(request, token);

        var result = StatusCode((int)HttpStatusCode.NoContent);
        return result;
    }

    protected async Task<IActionResult> ExcecuteQueryAsync<TInput, TOutput>(TInput request, CancellationToken token) where TInput : IRequest<TOutput>
    {
        var result = StatusCode((int)HttpStatusCode.OK, await RequestController.PublishAsync<TInput, TOutput>(request, token));
        return result;
    }
}