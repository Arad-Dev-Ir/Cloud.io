namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class KeywordAlreadyExistsException(string keywordTitle)
    : AppDomainException(Note.FormatByArguments(keywordTitle))
{
    private const string Note = "Keyword with Title '{0}' already exists";
}