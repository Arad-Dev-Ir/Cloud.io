namespace NewsManagement.Test.News.Intergrations;

using Cloudio.Core;
using NewsManagement.Core.News.AppServices;
using NewsManagement.Core.News.Contracts;
using NewsManagement.Data.Sql.News.Queries;

public class NewsDetailQueryHandlerTests(NewsQueryDbContextFixture fixture) : IClassFixture<NewsQueryDbContextFixture>
{
    private readonly NewsQueryDbContextFixture _fixture = fixture;

    [Fact]
    internal async Task Should_return_a_news_detail_when_news_exists()
    {
        // setup
        var context = await _fixture.BuildDbContext(UniqueIdentifier.GetIdAsString());

        var repository = new NewsQueryRepository(context);
        var systemUnderTest = new NewsDetailQueryHandler(repository);
        NewsDetailQuery request = new() { Id = 1 };

        // exercise
        var result = await systemUnderTest.HandleAsync(request, CancellationToken.None);

        // verify
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Title.Should().Be("Title");
        result.Body.Should().Be("Body");
        result.Description.Should().Be("Description");
        result.Keywords.Should().HaveCount(1);
    }

    [Fact]
    internal async Task Should_return_null_when_news_does_not_exist()
    {
        // setup
        var context = await _fixture.BuildDbContext(UniqueIdentifier.GetIdAsString());

        var repository = new NewsQueryRepository(context);
        var systemUnderTest = new NewsDetailQueryHandler(repository);
        NewsDetailQuery request = new() { Id = 2 };

        // exercise
        var result = await systemUnderTest.HandleAsync(request, CancellationToken.None);

        // verify
        result.Should().BeNull();
    }

    [Fact]
    internal async Task Should_return_news_list_when_news_exist()
    {
        // setup
        var context = await _fixture.BuildDbContext(UniqueIdentifier.GetIdAsString());

        var repository = new NewsQueryRepository(context);
        var systemUnderTest = new NewsListQueryHandler(repository);
        NewsListQuery request = new();

        // exercise
        var result = await systemUnderTest.HandleAsync(request, CancellationToken.None);

        // verify
        result.Should().NotBeNull();
        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(1);
    }
}