namespace NewsManagement.Data.Sql.News.Queries;

using Cloud.Core.Models;

public sealed record News : Record
{
    public long Id { get; init; }
    public Guid Code { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Body { get; init; }
    public DateTime CreatedDateTime { get; init; }

    public List<NewsKeyword> Keywords { get; init; } = [];
}