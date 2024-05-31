namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;

public class KeywordDeactivated : Event
{
    public Guid Code { get; private set; }
    public string Title { get; private set; } = Empty;
    public string Mode { get; private set; } = Empty;

    #region Initialize

    private KeywordDeactivated(Code code, Title title)
    => Initialize(code, title, delegate () { Mode = Models.Mode.Inactive.Value; });

    private void Initialize(Code code, Title title, Action? act = default)
    {
        Code = code.Value;
        Title = title.Value;

        act?.Invoke();
    }

    public static KeywordDeactivated Instance(string code, string title)
    => new(code, title);
    public static KeywordDeactivated Instance(Code code, Title title)
    => new(code, title);

    #endregion

    #region Methods

    #endregion
}