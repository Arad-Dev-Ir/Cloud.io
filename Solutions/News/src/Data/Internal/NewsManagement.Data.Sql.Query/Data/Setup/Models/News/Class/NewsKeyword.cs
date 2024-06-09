namespace NewsManagement.Data.Sql.News.Queries;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cloud.Core.Models;

public sealed record NewsKeyword : Record
{
    public long Id { get; init; }
    public Guid Code { get; init; }


    [ForeignKey(nameof(News))]
    public long NewsId { get; init; }
    public News News { get; init; }

    [ForeignKey(nameof(Keyword))]
    public Guid KeywordCode { get; init; }
    public Keyword Keyword { get; init; }
}

[Table("Keywords", Schema = "dbo")]
public sealed record Keyword : Record
{
    [Key]
    public Guid Code { get; init; }
    public string Title { get; init; }
}