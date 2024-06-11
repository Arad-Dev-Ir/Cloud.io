namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public sealed record RegisterNews : Command<long>
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Body { get; init; }
    public required IEnumerable<string> KeywordsCodes { get; init; }
}