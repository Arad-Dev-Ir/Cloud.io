namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public sealed record NewsRecordsResult : Record
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required DateTime RegistrationDate { get; init; }
}