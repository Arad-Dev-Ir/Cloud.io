namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordActivated(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Active.Value;
}