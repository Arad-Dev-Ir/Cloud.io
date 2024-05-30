namespace Cloud.Web.Core.Contract;

public sealed class QueryResponse<D> : AppServiceResponse, IQueryResponse<D>
{
    public D Data { get; set; }
}
