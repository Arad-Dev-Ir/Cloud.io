namespace NewsManagement.Core.News.Models;

using Cloud.Core.Models;

public sealed record NewsCreated(Guid Code, string Title, string Description, string Body, IEnumerable<string> Keywords) : Event
{
    public Guid Code { get; } = Code;
    public string Title { get; } = Title;
    public string Description { get; } = Description;
    public string Body { get; } = Body;
    public IEnumerable<string> Keywords { get; } = Keywords;
}