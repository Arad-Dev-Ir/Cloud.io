namespace NewsManagement.Core.News.Models;

using Cloudio.Core;
using Cloudio.Core.Models;

public class NewsKeywordsNullOrEmptyException(string newsTitle) : AppDomainException(Note.FormatByArguments(newsTitle))
{
    private const string Note = "Keywords cannot be null or empty. It is mandatory to select at least one keyword for '{0}'";
}