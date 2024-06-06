namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;

public record TitleAndModeSearchResult : Record
{
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Title { get; set; }
    public string Mode { get; set; }
}