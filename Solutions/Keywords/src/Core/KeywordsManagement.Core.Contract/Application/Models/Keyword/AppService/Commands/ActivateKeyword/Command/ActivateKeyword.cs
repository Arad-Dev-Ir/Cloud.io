namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record ActivateKeyword : Command
{
    public required long Id { get; init; }
}