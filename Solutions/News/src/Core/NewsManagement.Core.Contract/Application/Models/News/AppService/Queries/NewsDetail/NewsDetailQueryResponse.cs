namespace NewsManagement.Core.News.Contracts;

using Cloudio.Core.Models;

public sealed record NewsDetailQueryResponse : DataTransferObject
{
    public required long Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Body { get; init; }

    public required DateTime RegistrationDate { get; init; }

    public required List<KeywordQueryResponse> Keywords { get; init; } = [];
}

public sealed record KeywordQueryResponse : DataTransferObject
{
    public required Guid Code { get; init; }

    public required string Title { get; init; }
}