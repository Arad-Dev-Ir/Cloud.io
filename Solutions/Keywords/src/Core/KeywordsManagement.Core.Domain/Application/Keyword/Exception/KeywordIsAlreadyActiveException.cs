namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public class KeywordIsAlreadyActiveException() : AppDomainException(Note)
{
    private const string Note = "Cannot activate the Keyword, because it is already active";
}