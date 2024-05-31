namespace KeywordsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public class BasicInformationUnitOfWork : UnitOfWork<KeywordsManagementCommandContext>
{
    public BasicInformationUnitOfWork(KeywordsManagementCommandContext context) : base(context)
    { }
}