namespace NewsManagement.Test.News.Units;

using Cloudio.Core.Models;
using NewsManagement.Core.News.Models;

public class NewsTests
{
    [Fact]
    internal void Should_create_news_when_calling_create_instance_method()
    {
        // setup
        var title = NewsTitle.CreateInstance("news");
        var description = NewsDescription.CreateInstance("description");
        var body = NewsBody.CreateInstance("body");
        IEnumerable<Keyword> keywords = [Keyword.CreateInstance(Code.CreateInstance())];

        // exercise
        var news = News.CreateInstance(title, description, body, keywords);

        // verify
        news.Title.Should().Be(title);
        news.Description.Should().Be(description);
        news.Body.Should().Be(body);
        news.Keywords.Should().HaveCount(1);
    }

    [Fact]
    internal void Should_raise_event_when_the_news_is_created()
    {
        // exercise
        var news = News.CreateInstance("news", "description", "body", [Keyword.CreateInstance(Code.CreateInstance())]);

        // verify
        news.Events.Should().ContainItemsAssignableTo<NewsCreated>();
    }
}