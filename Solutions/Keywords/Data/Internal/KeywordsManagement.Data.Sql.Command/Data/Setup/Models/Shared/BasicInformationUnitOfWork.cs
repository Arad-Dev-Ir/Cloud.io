namespace KeywordsManagement.Data.Sql.Commands;

using Cloud.Web.Data.Sql.Command;

public class BasicInformationUnitOfWork : UnitOfWork<BasicInformationCommandContext>
{
    public BasicInformationUnitOfWork(BasicInformationCommandContext context) : base(context)
    { }
}