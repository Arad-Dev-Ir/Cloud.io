namespace NewsManagement.Test.News.Intergrations;

using Cloudio.Core;
using NewsManagement.Core.News.AppServices;
using NewsManagement.Core.News.Contracts;
using NewsManagement.Data.Sql.Commands;
using NewsManagement.Data.Sql.News.Commands;

public class RegisterNewsCommandHandlerTests(NewsCommandDbContextFixture fixture) : IClassFixture<NewsCommandDbContextFixture>
{
    private readonly NewsCommandDbContextFixture _fixture = fixture;

    [Fact]
    internal async Task Should_register_news_when_news_does_not_exist()
    {
        // setup
        var context = await _fixture.BuildDbContext(UniqueIdentifier.GetIdAsString());
        var repository = new NewsCommandRepository(context);
        var systemUnderTest = new RegisterNewsCommandHandler(repository, new NewsManagementUnitOfWork(context));
        RegisterNewsCommand command = new()
        {
            Title = "Title",
            Body = "Body",
            Description = "Description",
            KeywordsCodes = [UniqueIdentifier.GetIdAsString(), UniqueIdentifier.GetIdAsString()]
        };

        // exercise
        var result = await systemUnderTest.HandleAsync(command, CancellationToken.None);

        // verify
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);
        result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    internal async Task Should_throw_exception_when_news_already_exists()
    {
        // setup
        var context = await _fixture.BuildDbContext(UniqueIdentifier.GetIdAsString());
        var repository = new NewsCommandRepository(context);
        var systemUnderTest = new RegisterNewsCommandHandler(repository, new NewsManagementUnitOfWork(context));
        RegisterNewsCommand command = new()
        {
            Title = "Title",
            Body = "Body",
            Description = "Description",
            KeywordsCodes = [UniqueIdentifier.GetIdAsString(), UniqueIdentifier.GetIdAsString()]
        };
        var response = await systemUnderTest.HandleAsync(command, CancellationToken.None);

        // exercise
        var act = async () => await systemUnderTest.HandleAsync(command, CancellationToken.None);

        // verify
        await act.Should().ThrowExactlyAsync<NewsAlreadyExistsException>();
    }
}