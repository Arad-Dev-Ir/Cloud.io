namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record CreateKeyword : Command<long>
{
    public required string Title { get; init; }
}