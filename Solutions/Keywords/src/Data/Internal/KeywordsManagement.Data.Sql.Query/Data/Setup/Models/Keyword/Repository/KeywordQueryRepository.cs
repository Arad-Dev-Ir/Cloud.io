namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Core;
using Cloud.Web.Core.Contract;
using Cloud.Web.Data.Sql.Query;
using System.Threading.Tasks;
using Core.Keyword.Contracts;
using Sql.Queries;

public class KeywordQueryRepository : QueryRepository<KeywordsManagementQueryContext>, IKeywordQueryRepository
{
    public KeywordQueryRepository(KeywordsManagementQueryContext context) : base(context)
    { }

    public async Task<PagedData<TitleAndModeSearchResult>> Query(TitleAndModeSearch query, CancellationToken cancellationToken)
    {
        var result = new PagedData<TitleAndModeSearchResult>();
        var lookup = Context.Keywords.AsQueryable();

        var title = query.Title;
        var titleCondition = title.IsNotEmpty();
        lookup = lookup.Where(titleCondition, e => e.Title.Contains(title));

        var mode = query.Mode;
        var modeCondition = mode.IsNotEmpty();
        lookup = lookup.Where(modeCondition, e => e.Mode == mode);

        result.Records = await lookup
        .OrderBy(query.OrderBy, query.Ascending)
        .Skip(query.SkipCount)
        .Take(query.PageSize)
        .Select(e => new TitleAndModeSearchResult
        {
            Id = e.Id,
            Code = e.Code,
            Title = e.Title,
            Mode = e.Mode,
        })
        .ToListAsync(cancellationToken);

        var a = result.GetHashCode();
        result.TotalCount = query.NeedTotalCount ? lookup.Count() : default;
        var b = result.GetHashCode();

        return result;
    }
}