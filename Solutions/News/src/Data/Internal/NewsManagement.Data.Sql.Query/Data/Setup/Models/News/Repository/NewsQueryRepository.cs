namespace NewsManagement.Data.Sql.News.Queries;

using Microsoft.EntityFrameworkCore;
using Cloud.Core;
using Cloud.Web.Core.Contract;
using Cloud.Web.Data.Sql.Query;
using Core.News.Contracts;
using Sql.Queries;

public class NewsQueryRepository(NewsManagementQueryContext context) :
    QueryRepository<NewsManagementQueryContext>(context), INewsQueryRepository
{
    public async Task<NewsDetailResult> Query(NewsDetail query, CancellationToken cancellationToken)
    {
        var result = await Context
        .News
        .Include(e => e.Keywords)
        .ThenInclude(e => e.Keyword)
        .Where(e => e.Id == query.Id)
        .Select(e => new NewsDetailResult
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Body = e.Body,
            RegistrationDate = e.CreatedDateTime,

            Keywords = e.Keywords
            .Select(e =>
            new KeywordResult
            {
                Code = e.Keyword.Code,
                Title = e.Keyword.Title
            })
            .ToList()
        })
        .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<PagedData<NewsRecordsResult>> Query(NewsRecords query, CancellationToken cancellationToken)
    {
        var result = new PagedData<NewsRecordsResult>();
        var lookup = Context.News.AsQueryable();

        result.Records = await lookup
        .OrderBy(query.OrderBy, query.Ascending)
        .Skip(query.SkipCount)
        .Take(query.PageSize)
        .Select(e =>
        new NewsRecordsResult
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            RegistrationDate = e.CreatedDateTime
        })
        .ToListAsync(cancellationToken);
        result.TotalCount = query.NeedTotalCount ? lookup.Count() : default;

        return result;
    }
}
