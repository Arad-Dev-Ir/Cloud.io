namespace NewsManagement.Test.News.Intergrations;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsManagement.Data.Sql.Commands;

public class NewsCommandDbContextFixture : DatabaseFixture<NewsManagementCommandContext>
{
    protected override NewsManagementCommandContext BuildDbContext(DbContextOptions<NewsManagementCommandContext> options)
    => new(options);

    protected override Task Seed()
    => Task.CompletedTask;
}