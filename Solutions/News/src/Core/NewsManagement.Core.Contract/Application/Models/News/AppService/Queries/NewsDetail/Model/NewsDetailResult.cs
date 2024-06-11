namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public sealed record NewsDetailResult : Record
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Body { get; init; }
    public required List<KeywordResult> Keywords { get; init; } = [];
    public required DateTime RegistrationDate { get; init; }
}

public sealed record KeywordResult : Record
{
    public required Guid Code { get; init; }
    public required string Title { get; init; }
}