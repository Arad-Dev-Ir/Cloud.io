namespace NewsManagement.Data.Sql.News.Queries;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cloudio.Core.Models;

public sealed class NewsKeyword : Model
{
    public long Id { get; init; }

    public Guid Code { get; init; }

    [ForeignKey(nameof(News))]
    public long NewsId { get; init; }
    public News News { get; init; } = null!;

    [ForeignKey(nameof(Keyword))]
    public Guid KeywordCode { get; init; }
    public Keyword Keyword { get; init; } = null!;
}

[Table("Keywords", Schema = "dbo")]
public sealed class Keyword : Model
{
    [Key]
    public Guid Code { get; init; }

    public string Title { get; init; } = null!;

    public string State { get; init; } = null!;
}