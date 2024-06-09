namespace NewsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public sealed class NewsManagementUnitOfWork(NewsManagementCommandContext context) : UnitOfWork<NewsManagementCommandContext>(context)
{ }