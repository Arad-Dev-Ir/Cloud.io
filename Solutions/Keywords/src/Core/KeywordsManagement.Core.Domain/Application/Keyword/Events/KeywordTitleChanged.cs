namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordTitleChanged(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Preview.Value;
}