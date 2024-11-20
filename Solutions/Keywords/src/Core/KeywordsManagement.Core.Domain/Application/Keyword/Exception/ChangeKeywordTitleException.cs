namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public class ChangeKeywordTitleException() : AppDomainException(Note)
{
    private const string Note = "Cannot change the Keyword's title, because it is already inactive";
}