namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordActivated(Guid Code, string Title) : Event
{
    public string Mode { get; private set; } = Models.Mode.Active.Value;
}