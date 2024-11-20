namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class KeywordNotfoundException(long id) : AppDomainException(Note.FormatByArguments(id.ToString()))
{
    private const string Note = "Keyword with id '{0}' not found";
}
