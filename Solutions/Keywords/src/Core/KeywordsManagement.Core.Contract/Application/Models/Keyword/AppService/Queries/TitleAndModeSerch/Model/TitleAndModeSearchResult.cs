namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;

public record TitleAndModeSearchResult : Record
{
    public long Id { get; init; }
    public Guid Code { get; init; }
    public string Title { get; init; }
    public string State { get; init; }
}