namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public class TitleAndModeSearch : PageQuery<PagedData<TitleAndModeSearchResult>>
{
    public string Title { get; set; } = Empty;
    public string? Mode { get; set; } = Empty;
}