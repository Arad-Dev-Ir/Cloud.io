namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public record NewsRecordsResult : Record
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime RegistrationDate { get; set; }
}