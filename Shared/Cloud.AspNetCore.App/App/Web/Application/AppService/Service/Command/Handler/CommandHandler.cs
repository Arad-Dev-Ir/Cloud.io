namespace Cloud.Web.Core.AppService;

using Web.Core.Contract;

public abstract class CommandHandler<C, D> : ICommandHandler<C, D> where C : ICommand<D>
{
    protected readonly CommandResponse<D> Response = new();

    public CommandHandler() { }

    public abstract Task<CommandResponse<D>> ExecuteAsync(C command);

    #region Methods

    protected virtual Task<CommandResponse<D>> SetAsync(D data)
    {
        var status = data != null ? ServiceStatus.Ok : ServiceStatus.NotFound;
        var result = SetAsync(data, status);
        return result;
    }

    protected virtual async Task<CommandResponse<D>> OkAsync(D data)
    => await SetAsync(data, ServiceStatus.Ok);

    protected virtual Task<CommandResponse<D>> SetAsync(D data, ServiceStatus status)
    {
        Response.Data = data;
        Response.Status = status;
        var result = Task.FromResult(Response);
        return result;
    }

    protected virtual CommandResponse<D> Set(D data)
    {
        var status = data != null ? ServiceStatus.Ok : ServiceStatus.NotFound;
        var result = Set(data, status);
        return result;
    }

    protected virtual CommandResponse<D> Ok(D data)
    => Set(data, ServiceStatus.Ok);

    protected virtual CommandResponse<D> Set(D data, ServiceStatus status)
    {
        Response.Data = data;
        Response.Status = status;
        return Response;
    }

    protected void AddMessage(string message)
    => Response.AddMessage(message);

    #endregion
}

public abstract class CommandHandler<C> : ICommandHandler<C> where C : ICommand
{
    public CommandHandler() { }
    protected readonly CommandResponse Response = new();

    public abstract Task<CommandResponse> ExecuteAsync(C command);

    #region Methods

    protected virtual Task<CommandResponse> SetResponseAsync(ServiceStatus status)
    {
        Response.Status = status;
        return Task.FromResult(Response);
    }
    protected virtual CommandResponse SetResponse(ServiceStatus status)
    {
        Response.Status = status;
        return Response;
    }

    protected virtual Task<CommandResponse> OkAsync()
    {
        Response.Status = ServiceStatus.Ok;
        return Task.FromResult(Response);
    }
    protected virtual CommandResponse Ok()
    {
        Response.Status = ServiceStatus.Ok;
        return Response;
    }

    protected void AddMessage(string message)
    => Response.AddMessage(message);

    #endregion
}