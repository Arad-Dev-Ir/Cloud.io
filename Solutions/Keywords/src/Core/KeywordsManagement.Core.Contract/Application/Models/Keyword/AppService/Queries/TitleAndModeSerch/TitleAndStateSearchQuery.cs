namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Web.Core.Contract;

public sealed record TitleAndStateSearchQuery(string Title, string? State = default)
    : PageQuery<PagedData<TitleAndStateSearchQueryResponse>>;