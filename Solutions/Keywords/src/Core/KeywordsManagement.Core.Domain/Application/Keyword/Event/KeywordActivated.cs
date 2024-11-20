namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public sealed record KeywordActivated(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Active.Value;
}