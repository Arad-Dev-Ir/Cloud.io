namespace Cloud.Web.Core.AppService;

using Web.Core.Contract;

public abstract class QueryHandler<Q, D> : IQueryHandler<Q, D> where Q : IQuery<D>
{
    protected readonly QueryResponse<D> Response = new();

    public QueryHandler()
    { }

    public abstract Task<QueryResponse<D>> ExecuteAsync(Q query, CancellationToken cancellationToken);

    protected virtual Task<QueryResponse<D>> SetAsync(D data)
    {
        var status = data != null ? ServiceStatus.Ok : ServiceStatus.NotFound;
        var result = SetAsync(data, status);
        return result;
    }

    protected virtual Task<QueryResponse<D>> SetAsync(D data, ServiceStatus status)
    {
        Response.Data = data;
        Response.Status = status;
        var result = Task.FromResult(Response);
        return result;
    }

    protected virtual QueryResponse<D> Set(D data)
    {
        var status = data != null ? ServiceStatus.Ok : ServiceStatus.NotFound;
        var result = Set(data, status);
        return result;
    }
    protected virtual QueryResponse<D> Set(D data, ServiceStatus status)
    {
        Response.Data = data;
        Response.Status = status;
        return Response;
    }

    protected void AddMessage(string message)
    => Response.AddMessage(message);
}
