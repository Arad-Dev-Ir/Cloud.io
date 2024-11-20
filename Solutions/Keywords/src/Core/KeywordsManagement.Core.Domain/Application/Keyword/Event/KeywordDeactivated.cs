namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;

public sealed record KeywordDeactivated(Guid Code, string Title) : Event
{
    public string State { get; } = KeywordState.Inactive.Value;
}