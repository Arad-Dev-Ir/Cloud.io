namespace Cloud.Web.Data.Sql.Query;

using Core.Contract;

public class QueryRepository<C> : IQueryRepository where C : QueryContext
{
    public QueryRepository(C context) => Context = context;
    protected readonly C Context;
}
