namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public class ChangeKeywordTitle : Command
{
    public long Id { get; set; }
    public string Title { get; set; } = Empty;
}