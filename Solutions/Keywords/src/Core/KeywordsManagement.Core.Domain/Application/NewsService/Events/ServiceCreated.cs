namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Core.Models;

public record ServiceCreated(Guid Code, string Title, string Name) : Event;