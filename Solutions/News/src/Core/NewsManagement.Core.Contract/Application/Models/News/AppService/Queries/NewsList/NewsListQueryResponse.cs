namespace NewsManagement.Core.News.Contracts;

using Cloudio.Core.Models;

public sealed record NewsListQueryResponse : DataTransferObject
{
    public required long Id { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required DateTime RegistrationDate { get; init; }
}