namespace NewsManagement.Core.News.Models;

using Cloudio.Web.Core;

public sealed class News : Module
{
    public NewsTitle Title { get; private set; }

    public NewsDescription Description { get; private set; }

    public NewsBody Body { get; private set; }

    private readonly List<Keyword> _keywords = [];
    public IReadOnlyCollection<Keyword> Keywords => [.. _keywords];

    #region Object Instantiation

    private News()
    { }
    private News(NewsTitle title, NewsDescription description, NewsBody body, IEnumerable<Keyword> keywords)
    {
        if (!keywords.Any())
            throw new NewsKeywordsNullOrEmptyException(title.Value);

        Title = title;
        Description = description;
        Body = body;
        _keywords.AddRange(keywords);

        OnNewsCreated();
    }

    public static News CreateInstance(NewsTitle title, NewsDescription description, NewsBody body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    public static News CreateInstance(string title, string description, string body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    #endregion

    #region Methods

    private void OnNewsCreated()
    => AddEvent(new NewsCreated(Code.Value, Title.Value, Description.Value, Body.Value, _keywords.Select(e => e.KeywordCode.ToString()).ToList()));

    #endregion
}