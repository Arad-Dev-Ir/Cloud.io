namespace Cloud.Web.Endpoint.API;

using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Core.AppService;
using Web.Core.Contract;

[ApiController]
public class ApiController : Controller
{
    protected CommandPipeline CommandDispatcher => HttpContext.GetCommandDispatcher();
    protected QueryPipeline QueryDispatcher => HttpContext.GetQueryDispatcher();
    protected EventPipeline EventDispatcher => HttpContext.GetEventDispatcher();
    //protected ToolkitService ToolkitService => HttpContext.GetToolkitService();


    protected async Task<IActionResult> Create<C, D>(C command) where C : ICommand<D>
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C, D>(command);

        var status = response.Status;
        result = status == ServiceStatus.Ok ? StatusCode((int)HttpStatusCode.Created, response.Data) : BadRequest(response.Messages);
        return result;
    }
    protected async Task<IActionResult> Create<C>(C command) where C : ICommand
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C>(command);

        var status = response.Status;
        result = status == ServiceStatus.Ok ? StatusCode((int)HttpStatusCode.Created) : BadRequest(response.Messages);
        return result;
    }


    protected async Task<IActionResult> Edit<C, D>(C command) where C : ICommand<D>
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C, D>(command);

        var status = response.Status;
        if (status == ServiceStatus.Ok)
            result = StatusCode((int)HttpStatusCode.OK, response.Data);
        else if (status == ServiceStatus.NotFound)
            result = StatusCode((int)HttpStatusCode.NotFound, command);
        result = BadRequest(response.Messages);

        return result;
    }
    protected async Task<IActionResult> Edit<C>(C command) where C : ICommand
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C>(command);

        var status = response.Status;
        if (status == ServiceStatus.Ok)
            result = StatusCode((int)HttpStatusCode.OK);
        else if (status == ServiceStatus.NotFound)
            result = StatusCode((int)HttpStatusCode.NotFound, command);
        else
            result = BadRequest(response.Messages);

        return result;
    }


    protected async Task<IActionResult> Delete<C, D>(C command) where C : ICommand<D>
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C, D>(command);

        var status = response.Status;
        if (status == ServiceStatus.Ok)
            result = StatusCode((int)HttpStatusCode.NoContent, response.Data);
        else if (status == ServiceStatus.NotFound)
            result = StatusCode((int)HttpStatusCode.NotFound, command);
        else
            result = BadRequest(response.Messages);

        return result;
    }
    protected async Task<IActionResult> Delete<C>(C command) where C : ICommand
    {
        var result = default(IActionResult);
        var response = await CommandDispatcher.ExecuteAsync<C>(command);

        var status = response.Status;
        if (status == ServiceStatus.Ok)
            result = StatusCode((int)HttpStatusCode.NoContent);
        else if (status == ServiceStatus.NotFound)
            result = StatusCode((int)HttpStatusCode.NotFound, command);
        else
            result = BadRequest(response.Messages);

        return result;
    }

    protected async Task<IActionResult> ExcecuteQuery<Q, D>(Q query) where Q : IQuery<D>
    {
        var result = default(IActionResult);
        var response = await QueryDispatcher.ExecuteAsync<Q, D>(query);

        var status = response.Status;
        if (status == ServiceStatus.InvalidDomainState || status == ServiceStatus.ValidationError)
            result = BadRequest(response.Messages);
        else if (status == ServiceStatus.NotFound || response.Data == null)
            result = StatusCode((int)HttpStatusCode.NoContent);
        else if (status == ServiceStatus.Ok)
            result = Ok(response.Data);
        else
            result = BadRequest(response.Messages);

        return result;
    }
}
