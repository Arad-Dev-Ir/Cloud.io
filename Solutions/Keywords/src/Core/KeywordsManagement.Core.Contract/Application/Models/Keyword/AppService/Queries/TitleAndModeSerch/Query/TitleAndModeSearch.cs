namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record TitleAndModeSearch : PageQuery<PagedData<TitleAndModeSearchResult>>
{
    public required string Title { get; init; }
    public string? State { get; init; }
}