namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using Cloud.Core.Models;

public class Keyword : Model
{
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Title { get; set; } = Empty;
    public string Mode { get; set; } = Empty;
}