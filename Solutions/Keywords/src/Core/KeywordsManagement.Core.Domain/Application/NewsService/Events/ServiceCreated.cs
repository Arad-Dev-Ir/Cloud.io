namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Core.Models;

public sealed record ServiceCreated(Guid Code, string Title, string Name) : Event
{
    public Guid Code { get; } = Code;
    public string Title { get; } = Title;
    public string Name { get; } = Name;
}