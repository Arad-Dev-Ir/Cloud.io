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
    => Initialize(title, description, body, keywords, () => OnCreateBlog(title, description, body, keywords));

    private void Initialize(Title title, Description description, Body body, IEnumerable<Keyword> keywords, Action? act = default)
    {
        Title = title;
        Description = description;
        Body = body;
        _keywords.AddRange(keywords);

        act?.Invoke();
    }

    public static News Instance(Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    public static News Instance(string title, string description, string body, IEnumerable<Keyword> keywords)
    => new(title, description, body, keywords);

    #endregion

    #region Methods

    private void OnCreateBlog(Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    => AddEvent(new NewsCreated(Code.Value, title.Value, description.Value, body.Value, keywords.Select(e => e.KeywordCode.ToString()).ToList()));

    #endregion
}