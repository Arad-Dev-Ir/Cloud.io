namespace KeywordsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public class KeywordsManagementUnitOfWork : UnitOfWork<KeywordsManagementCommandContext>
{
    public KeywordsManagementUnitOfWork(KeywordsManagementCommandContext context) : base(context)
    { }
}