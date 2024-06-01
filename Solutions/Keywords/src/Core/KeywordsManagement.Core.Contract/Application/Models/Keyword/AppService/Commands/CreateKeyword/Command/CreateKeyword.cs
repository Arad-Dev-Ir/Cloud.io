namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public class CreateKeyword : Command<long>
{
    public string Title { get; set; } = Empty;
}