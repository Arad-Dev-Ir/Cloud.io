namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordDeactivated(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Inactive.Value;
}