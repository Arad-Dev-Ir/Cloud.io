namespace NewsManagement.Data.Sql.Commands;

using Cloudio.Web.Data.Sql.Command;

public sealed class NewsManagementUnitOfWork(NewsManagementCommandContext context)
    : UnitOfWork<NewsManagementCommandContext>(context)
{ }