namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public sealed record RegisterNews : Command<long>
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Body { get; init; }
    public IEnumerable<string> KeywordsCodes { get; init; }
}