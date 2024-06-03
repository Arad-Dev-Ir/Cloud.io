namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record TitleAndModeSearch(string Title, string? Mode) : PageQuery<PagedData<TitleAndModeSearchResult>>;