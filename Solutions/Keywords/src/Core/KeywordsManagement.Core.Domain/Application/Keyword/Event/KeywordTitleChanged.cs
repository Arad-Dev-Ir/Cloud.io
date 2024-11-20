namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public sealed record KeywordTitleChanged(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Preview.Value;
}