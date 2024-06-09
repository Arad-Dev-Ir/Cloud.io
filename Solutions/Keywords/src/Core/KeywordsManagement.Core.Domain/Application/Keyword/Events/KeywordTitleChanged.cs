namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public sealed record KeywordTitleChanged(Guid Code, string Title) : Event
{
    public Guid Code { get; } = Code;
    public string Title { get; } = Title;
    public string State { get; } = KeywordState.Preview.Value;
}