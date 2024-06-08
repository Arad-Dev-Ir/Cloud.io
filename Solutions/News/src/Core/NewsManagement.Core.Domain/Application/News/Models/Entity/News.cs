namespace NewsManagement.Core.News.Models;

using Cloud.Web.Core;

public class News : Module
{
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public Body Body { get; private set; }

    private readonly List<Keyword> _keywords = [];
    public IReadOnlyList<Keyword> Keywords => _keywords;

    #region Initialize

    private News()
    { }
    private News(Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    {
        Title = title;
        Description = description;
        Body = body;
        _keywords.AddRange(keywords);

        OnCreateBlog();
    }

    public static News Instance(Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    public static News Instance(string title, string description, string body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    #endregion

    #region Methods

    private void OnCreateBlog()
    => AddEvent(new NewsCreated(Code.Value, Title.Value, Description.Value, Body.Value, _keywords.Select(e => e.KeywordCode.ToString()).ToList()));

    #endregion
}