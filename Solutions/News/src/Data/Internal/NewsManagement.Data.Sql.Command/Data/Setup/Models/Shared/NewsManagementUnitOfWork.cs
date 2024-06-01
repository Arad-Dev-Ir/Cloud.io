namespace NewsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public class NewsManagementUnitOfWork : UnitOfWork<NewsManagementCommandContext>
{
    public NewsManagementUnitOfWork(NewsManagementCommandContext context) : base(context)
    { }
}