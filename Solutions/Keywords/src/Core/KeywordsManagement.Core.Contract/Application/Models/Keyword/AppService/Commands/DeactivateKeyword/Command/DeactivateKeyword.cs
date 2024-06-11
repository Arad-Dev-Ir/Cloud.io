namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record DeactivateKeyword : Command
{
    public required long Id { get; init; }
}