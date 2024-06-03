namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;

public record TitleAndModeSearchResult : TransferModel
{
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Title { get; set; }
    public string Mode { get; set; }
}