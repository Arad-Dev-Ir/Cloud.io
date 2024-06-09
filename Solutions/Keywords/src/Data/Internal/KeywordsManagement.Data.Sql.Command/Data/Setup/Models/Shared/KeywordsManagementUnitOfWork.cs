namespace KeywordsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public class KeywordsManagementUnitOfWork(KeywordsManagementCommandContext context) :
    UnitOfWork<KeywordsManagementCommandContext>(context)
{ }