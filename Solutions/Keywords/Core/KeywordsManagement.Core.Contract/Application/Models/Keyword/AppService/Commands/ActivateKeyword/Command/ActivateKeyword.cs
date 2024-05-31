namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public class ActivateKeyword : Command
{
    public long Id { get; set; }
}