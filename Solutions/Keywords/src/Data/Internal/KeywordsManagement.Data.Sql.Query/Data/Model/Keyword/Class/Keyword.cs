namespace KeywordsManagement.Data.Sql.Keyword.Queries;

using Cloudio.Core.Models;

public sealed record Keyword : DataTransferObject
{
    public long Id { get; init; }

    public Guid Code { get; init; }

    public string Title { get; init; } = null!;

    public string State { get; init; } = null!;
}