namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public class NewsDetailResult : Model
{
    public long Id { get; set; }
    public string Title { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public string Body { get; set; } = Empty;
    public List<KeywordResult> Keywords { get; set; } = [];
    public DateTime RegistrationDate { get; set; }
}

public class KeywordResult : Model
{
    public Guid Code { get; set; }
    public string Title { get; set; } = Empty;
}