namespace NewsManagement.Core.News.Models;

using Cloudio.Core.Models;

public sealed record NewsCreated(Guid Code, string Title, string Description, string Body, IEnumerable<string> Keywords) : Event;