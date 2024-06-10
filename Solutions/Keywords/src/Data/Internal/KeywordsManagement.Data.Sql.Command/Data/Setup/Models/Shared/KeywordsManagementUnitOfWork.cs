namespace KeywordsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public sealed class KeywordsManagementUnitOfWork(KeywordsManagementCommandContext context) :
    UnitOfWork<KeywordsManagementCommandContext>(context)
{ }