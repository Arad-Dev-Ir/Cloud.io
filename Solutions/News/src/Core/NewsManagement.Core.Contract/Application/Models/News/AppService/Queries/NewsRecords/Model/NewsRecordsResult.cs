namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public sealed record NewsRecordsResult : Record
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime RegistrationDate { get; init; }
}