namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public record KeywordDeactivated(Guid Code, string Title) : Event
{
    public string Mode { get; private set; } = Models.Mode.Inactive.Value;
}