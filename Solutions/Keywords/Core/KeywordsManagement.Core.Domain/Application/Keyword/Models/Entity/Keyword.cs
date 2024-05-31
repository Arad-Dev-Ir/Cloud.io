namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;
using Cloud.Web.Core;

public class Keyword : Module
{
    public Title Title { get; private set; }
    public Mode Mode { get; private set; } = Mode.Preview;

    #region Initialize

    private Keyword(Title title)
    => Initialize(title, () =>
    {
        Mode = Mode.Preview;
        OnCreateKeyword();
    });

    private void Initialize(Title title, Action? act = default)
    {
        Title = title;
        Initialize(act);
    }
    private void Initialize(Action? act = default)
    => act?.Invoke();

    public static Keyword Instance(Title title)
    => new(title);
    public static Keyword Instance(string title)
    => new(title);

    #endregion

    #region Methods

    private const string modeString = nameof(Mode);
    public void ChangeTitle(Title title)
    {
        var action = nameof(ChangeTitle);
        var mode = Mode.Inactive;
        if (Mode == mode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is {2}.", action, modeString, mode.Value);

        Initialize(title, () =>
        {
            Mode = Mode.Preview;
            OnChangeKeywordTitle();
        });
    }

    public void Activate()
    {
        var action = nameof(Activate);
        var mode = Mode.Active;
        if (Mode == mode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is already {2}.", action, modeString, mode.Value);

        Initialize(() =>
        {
            Mode = Mode.Active;
            OnActivateKeyword();
        });
    }

    public void Deactivate()
    {
        var action = nameof(Deactivate);
        var mode = Mode.Inactive;
        if (Mode == mode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is already {2}.", action, modeString, mode.Value);

        Initialize(() =>
        {
            Mode = Mode.Inactive;
            OnDeactivateKeyword();
        });
    }

    private void OnCreateKeyword()
    => AddEvent(KeywordCreated.Instance(Code, Title));

    private void OnChangeKeywordTitle()
    => AddEvent(KeywordTitleChanged.Instance(Code, Title));

    private void OnActivateKeyword()
    => AddEvent(KeywordActivated.Instance(Code, Title));

    private void OnDeactivateKeyword()
    => AddEvent(KeywordDeactivated.Instance(Code, Title));

    #endregion
}