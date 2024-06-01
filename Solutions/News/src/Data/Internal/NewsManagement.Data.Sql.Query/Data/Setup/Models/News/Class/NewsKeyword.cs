namespace NewsManagement.Data.Sql.News.Queries;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cloud.Core.Models;

public class NewsKeyword : Model
{
    public long Id { get; set; }
    public Guid Code { get; set; }


    [ForeignKey(nameof(News))]
    public long NewsId { get; set; }
    public News News { get; set; }

    [ForeignKey(nameof(Keyword))]
    public Guid KeywordCode { get; set; }
    public Keyword Keyword { get; set; }
}

[Table("Keywords", Schema = "dbo")]
public class Keyword : Model
{
    [Key]
    public Guid Code { get; set; }
    public string Title { get; set; } = Empty;
}