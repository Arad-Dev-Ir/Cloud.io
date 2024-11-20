namespace KeywordsManagement.Core.NewsService.Models;

using Cloudio.Core.Models;

public sealed record ServiceCreated(Guid Code, string Title, string Name) : Event;