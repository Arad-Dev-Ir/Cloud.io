namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordCreated(Guid Code, string Title) : Event
{
    public string Mode { get; private set; } = Models.Mode.Preview.Value;
}