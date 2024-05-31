namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public class DeactivateKeyword : Command
{
    public long Id { get; set; }
}