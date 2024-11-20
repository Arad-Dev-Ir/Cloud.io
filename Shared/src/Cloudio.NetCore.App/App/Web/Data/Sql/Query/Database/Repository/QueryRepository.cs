namespace Cloudio.Web.Data.Sql.Query;

using Cloudio.Web.Core.Contract;

public class QueryRepository<C>(C context) : IQueryRepository where C : QueryContext
{
    protected readonly C Context = context;
}