namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cloudio.Core;
using Cloudio.Web.Core.Contract;
using Cloudio.Web.Data.Sql.Query;
using KeywordsManagement.Core.Keyword.Contracts;
using KeywordsManagement.Data.Sql.Queries;

public class KeywordQueryRepository(KeywordsManagementQueryContext context)
    : QueryRepository<KeywordsManagementQueryContext>(context), IKeywordQueryRepository
{
    public async Task<PagedData<TitleAndStateSearchQueryResponse>> Query(TitleAndStateSearchQuery query, CancellationToken token)
    {
        PagedData<TitleAndStateSearchQueryResponse> result;

        var keywords = Context.Keywords.AsQueryable();

        var title = query.Title;
        var titleCondition = title.IsNotEmpty();
        keywords = keywords.Where(titleCondition, e => e.Title.Contains(title));

        var state = query.State ?? string.Empty;
        var stateCondition = state.IsNotEmpty();
        keywords = keywords.Where(stateCondition, e => e.State == state);

        var pageSize = query.PageSize;

        var items = await keywords
        .OrderBy(query.OrderBy, query.Ascending)
        .Skip(query.SkipCount)
        .Take(pageSize)
        .Select(e => new TitleAndStateSearchQueryResponse(e.Id, e.Code, e.Title, e.State))
        .ToListAsync(token);

        var totalCount = query.NeedTotalCount ? keywords.Count() : default;
        result = new() { Items = items, TotalCount = totalCount, Page = query.Page, PageSize = pageSize };

        return result;
    }
}