namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record ChangeKeywordTitle : Command
{
    public required long Id { get; init; }
    public required string Title { get; init; }
}