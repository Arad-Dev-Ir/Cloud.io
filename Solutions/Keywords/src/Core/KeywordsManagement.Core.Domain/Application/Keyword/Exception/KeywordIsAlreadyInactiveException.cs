namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public class KeywordIsAlreadyInactiveException() : AppDomainException(Note)
{
    private const string Note = "Cannot deactivate the Keyword, because it is already inactive";
}