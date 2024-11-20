namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Web.Core.Contract;

public interface IKeywordQueryRepository : IQueryRepository
{
    Task<PagedData<TitleAndStateSearchQueryResponse>> Query(TitleAndStateSearchQuery query, CancellationToken token);
}