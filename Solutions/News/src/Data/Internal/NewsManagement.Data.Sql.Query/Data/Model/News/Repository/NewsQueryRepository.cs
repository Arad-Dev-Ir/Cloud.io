namespace NewsManagement.Data.Sql.News.Queries;

using Microsoft.EntityFrameworkCore;
using Cloudio.Core;
using Cloudio.Web.Core.Contract;
using Cloudio.Web.Data.Sql.Query;
using NewsManagement.Core.News.Contracts;
using NewsManagement.Data.Sql.Queries;

public class NewsQueryRepository(NewsManagementQueryContext context)
    : QueryRepository<NewsManagementQueryContext>(context), INewsQueryRepository
{
    public async Task<NewsDetailQueryResponse> Query(NewsDetailQuery query, CancellationToken token)
    {
        var result = await Context
        .News
        .Include(e => e.Keywords)
        .ThenInclude(e => e.Keyword)
        .Where(e => e.Id == query.Id)
        .Select(e => new NewsDetailQueryResponse
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Body = e.Body,
            RegistrationDate = e.CreatedDateTime,

            Keywords = e.Keywords
            .Select(e =>
            new KeywordQueryResponse
            {
                Code = e.Keyword.Code,
                Title = e.Keyword.Title
            })
            .ToList()
        })
        .FirstOrDefaultAsync(token);

        return result ?? default!;
    }

    public async Task<PagedData<NewsListQueryResponse>> Query(NewsListQuery query, CancellationToken token)
    {
        PagedData<NewsListQueryResponse> result;
        var news = Context.News.AsQueryable();

        var pageSize = query.PageSize;

        var items = await news
        .OrderBy(query.OrderBy, query.Ascending)
        .Skip(query.SkipCount)
        .Take(pageSize)
        .Select(e =>
        new NewsListQueryResponse
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            RegistrationDate = e.CreatedDateTime
        })
        .ToListAsync(token);

        var totalCount = query.NeedTotalCount ? news.Count() : default;
        result = new() { Items = items, TotalCount = totalCount, Page = query.Page, PageSize = pageSize };

        return result;
    }
}
