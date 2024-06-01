namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public class KeywordTitleChanged : Event
{
    public Guid Code { get; private set; }
    public string Title { get; private set; } = Empty;
    public string Mode { get; private set; } = Empty;

    #region Initialize

    private KeywordTitleChanged(Code code, Title title)
    => Initialize(code, title, delegate () { Mode = Models.Mode.Preview.Value; });

    private void Initialize(Code code, Title title, Action? act = default)
    {
        Code = code.Value;
        Title = title.Value;

        act?.Invoke();
    }

    public static KeywordTitleChanged Instance(string code, string title)
    => new(code, title);
    public static KeywordTitleChanged Instance(Code code, Title title)
    => new(code, title);

    #endregion

    #region Methods

    #endregion
}