namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Core;
using Cloud.Web.Core.Contract;
using Cloud.Web.Data.Sql.Query;
using System.Threading.Tasks;
using Core.Keyword.Contracts;
using Sql.Queries;

public class KeywordQueryRepository(KeywordsManagementQueryContext context) :
    QueryRepository<KeywordsManagementQueryContext>(context), IKeywordQueryRepository
{
    public async Task<PagedData<TitleAndModeSearchResult>> Query(TitleAndModeSearch query, CancellationToken cancellationToken)
    {
        PagedData<TitleAndModeSearchResult> result;
        var lookup = Context.Keywords.AsQueryable();

        var title = query.Title;
        var titleCondition = title.IsNotEmpty();
        lookup = lookup.Where(titleCondition, e => e.Title.Contains(title));

        var state = query.State;
        var modeCondition = state.IsNotEmpty();
        lookup = lookup.Where(modeCondition, e => e.State == state);

        var pageSize = query.PageSize;

        var items = await lookup
         .OrderBy(query.OrderBy, query.Ascending)
         .Skip(query.SkipCount)
         .Take(pageSize)
         .Select(e => new TitleAndModeSearchResult
         {
             Id = e.Id,
             Code = e.Code,
             Title = e.Title,
             State = e.State,
         })
         .ToListAsync(cancellationToken);

        var totalCount = query.NeedTotalCount ? lookup.Count() : default;
        result = new() { Items = items, TotalCount = totalCount, Page = query.Page, PageSize = pageSize };

        return result;
    }
}