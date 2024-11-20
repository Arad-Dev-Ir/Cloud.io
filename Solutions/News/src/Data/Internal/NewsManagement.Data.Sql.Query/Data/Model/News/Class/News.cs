namespace NewsManagement.Data.Sql.News.Queries;

using Cloudio.Core.Models;

public sealed record News : DataTransferObject
{
    public long Id { get; init; }

    public Guid Code { get; init; }

    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Body { get; init; } = null!;

    public DateTime CreatedDateTime { get; init; }

    public List<NewsKeyword> Keywords { get; init; } = [];
}