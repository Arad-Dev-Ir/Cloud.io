namespace NewsManagement.Core.News.Models;

using Cloud.Core.Models;

public class NewsCreated : Event
{
    public Guid Code { get; private set; }
    public string Title { get; private set; } = Empty;
    public string Description { get; private set; } = Empty;
    public string Body { get; private set; } = Empty;
    public List<string> Keywords { get; private set; } = [];

    #region Initialize

    private NewsCreated(Code code, Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    => Initialize(code, title, description, body, keywords);

    void Initialize(Code code, Title title, Description description, Body body, IEnumerable<Keyword> keywords, Action? act = default)
    {
        Code = code.Value;
        Title = title.Value;
        Description = description.Value;
        Body = body.Value;
        Keywords.AddRange(
        keywords.
        Select(e => e.KeywordCode.ToString())
        .ToList()
        );

        act?.Invoke();
    }

    public static NewsCreated Instance(Code code, Title title, Description description, Body body, IEnumerable<Keyword> keywords)
    => new(code, title, description, body, keywords);
    public static NewsCreated Instance(string code, string title, string description, string body, IEnumerable<Keyword> keywords)
    => new(code, title, description, body, keywords);

    #endregion

    #region Methods

    #endregion
}
