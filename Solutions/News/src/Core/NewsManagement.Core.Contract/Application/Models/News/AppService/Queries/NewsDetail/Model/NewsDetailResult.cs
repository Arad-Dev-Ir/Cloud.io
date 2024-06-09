namespace NewsManagement.Core.News.Contracts;

using Cloud.Core.Models;

public sealed record NewsDetailResult : Record
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string Body { get; init; }
    public List<KeywordResult> Keywords { get; init; } = [];
    public DateTime RegistrationDate { get; init; }
}

public sealed record KeywordResult : Record
{
    public Guid Code { get; init; }
    public string Title { get; init; }
}