namespace NewsManagement.Test.News.Intergrations;

using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Cloudio.Core;
using NewsManagement.Data.Sql.News.Queries;
using NewsManagement.Data.Sql.Queries;

public class NewsQueryDbContextFixture : DatabaseFixture<NewsManagementQueryContext>
{
    private readonly TransactionScope _transaction;
    private NewsManagementQueryContext _context = null!;

    public NewsQueryDbContextFixture()
    => _transaction = new();

    protected override NewsManagementQueryContext BuildDbContext(DbContextOptions<NewsManagementQueryContext> options)
     => _context = new NewsManagementQueryContext(options);


    protected override async Task Seed()
    => await Initialize();

    async Task Initialize()
    {
        var news = new News
        {
            Id = 1,
            Title = "Title",
            Body = "Body",
            Description = "Description",
            Code = UniqueIdentifier.GetId(),
            CreatedDateTime = DateTime.UtcNow,
        };
        _context.News.Add(news);

        List<Keyword> keywords =
        [
            new Keyword
            {
                Title = "Title",
                Code = UniqueIdentifier.GetId(),
                State = "State"
            }
        ];

        List<NewsKeyword> newsKeywords = [];
        keywords.ForEach(
          e =>
          {
              var newsKeyword = new NewsKeyword
              {
                  Id = 1,
                  Code = UniqueIdentifier.GetId(),
                  NewsId = 1,
                  KeywordCode = e.Code,
                  Keyword = e
              };
              newsKeywords.Add(newsKeyword);
          });

        _context.NewsKeywords.AddRange(newsKeywords);
        await _context.SaveChangesAsync(CancellationToken.None);
    }

    public override void Dispose()
    {
        _transaction.Dispose();
        GC.SuppressFinalize(this);
    }
}