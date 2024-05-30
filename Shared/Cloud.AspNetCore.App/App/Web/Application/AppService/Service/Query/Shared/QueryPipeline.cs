namespace Cloud.Web.Core.AppService;

using Web.Core.Contract;

public abstract class QueryPipeline(IServiceProvider serviceProvider) : Procedure<QueryPipeline>(serviceProvider)
{
    public abstract Task<QueryResponse<T>> ExecuteAsync<Q, T>(Q query) where Q : IQuery<T>;
}