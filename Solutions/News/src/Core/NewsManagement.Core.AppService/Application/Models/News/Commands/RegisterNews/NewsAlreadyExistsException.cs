namespace NewsManagement.Core.News.AppServices;

using Cloudio.Core;
using Cloudio.Core.Models;

public sealed class NewsAlreadyExistsException(string newsTitle) : AppDomainException(Note.FormatByArguments(newsTitle))
{
    private const string Note = "News with Title '{0}' already exists";
}