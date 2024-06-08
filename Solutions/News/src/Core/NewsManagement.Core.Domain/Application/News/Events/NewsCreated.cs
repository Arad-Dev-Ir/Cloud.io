namespace NewsManagement.Core.News.Models;

using Cloud.Core.Models;

public record NewsCreated(Guid Code, string Title, string Description, string Body, IEnumerable<string> Keywords) : Event;