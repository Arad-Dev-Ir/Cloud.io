namespace Cloud.Web.Core.Contract;

public interface IQueryHandler<in Q, D> where Q : IQuery<D>
{
    Task<QueryResponse<D>> ExecuteAsync(Q query);
}