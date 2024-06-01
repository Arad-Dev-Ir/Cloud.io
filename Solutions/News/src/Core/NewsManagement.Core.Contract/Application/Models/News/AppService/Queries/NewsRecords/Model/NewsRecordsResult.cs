namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public class NewsRecordsResult : Model
{
    public long Id { get; set; }
    public string Title { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public DateTime RegistrationDate { get; set; }
}