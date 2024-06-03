namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Web.Core.Contract;

public record ChangeKeywordTitle(long Id, string Title) : Command;