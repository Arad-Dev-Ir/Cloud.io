namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public record NewsRecordsResult : TransferModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime RegistrationDate { get; set; }
}