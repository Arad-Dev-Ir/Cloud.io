namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using Cloud.Core.Models;

public sealed record Keyword : Record
{
    public long Id { get; init; }
    public Guid Code { get; init; }
    public string Title { get; init; }
    public string State { get; init; }
}