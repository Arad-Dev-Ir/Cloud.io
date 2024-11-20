namespace KeywordsManagement.Data.Sql.Commands;

using Cloudio.Web.Data.Sql.Command;

public sealed class KeywordsManagementUnitOfWork(KeywordsManagementCommandDbContext context)
    : UnitOfWork<KeywordsManagementCommandDbContext>(context)
{ }