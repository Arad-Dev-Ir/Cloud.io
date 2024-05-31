namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public interface IKeywordQueryRepository : IQueryRepository
{
    Task<PagedData<TitleAndModeSearchResult>> Query(TitleAndModeSearch query);
}