namespace KeywordsManagement.Core.NewsService.AppServices;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsServiceAlreadyExistsException(string newsServiceName)
    : AppDomainException(Note.FormatByArguments(newsServiceName))
{
    private const string Note = "NewsService with Name '{0}' already exists";
}