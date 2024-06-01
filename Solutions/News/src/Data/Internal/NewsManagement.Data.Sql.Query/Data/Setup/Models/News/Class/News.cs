namespace NewsManagement.Data.Sql.News.Queries;

using Cloud.Core.Models;

public class News : Model
{
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Title { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public string Body { get; set; } = Empty;
    public DateTime CreatedDateTime { get; set; }

    public List<NewsKeyword> Keywords { get; set; } = [];
}