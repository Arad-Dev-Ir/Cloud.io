namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public record NewsDetailResult : Record
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public List<KeywordResult> Keywords { get; set; } = [];
    public DateTime RegistrationDate { get; set; }
}

public record KeywordResult : Record
{
    public Guid Code { get; set; }
    public string Title { get; set; }
}