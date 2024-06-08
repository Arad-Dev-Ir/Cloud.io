namespace KeywordsManagement.Core.Keyword.Models;

using Cloud.Core.Models;
using Cloud.Web.Core;

public class Keyword : Module
{
    public Title Title { get; private set; }
    public KeywordState State { get; private set; }

    #region Initialize

    private Keyword()
    { }
    private Keyword(Title title)
    {
        Title = title;
        State = KeywordState.Preview;
        OnCreateKeyword();
    }

    public static Keyword Instance(Title title)
    => new(title);
    public static Keyword Instance(string title)
    => new(title);

    #endregion

    #region Methods

    private const string stateString = nameof(State);
    public void ChangeTitle(Title title)
    {
        var action = nameof(ChangeTitle);
        var inactiveMode = KeywordState.Inactive;
        if (State == inactiveMode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is {2}.", action, stateString, inactiveMode.Value);

        Title = title;
        State = KeywordState.Preview;
        OnChangeKeywordTitle();
    }

    public void Activate()
    {
        var action = nameof(Activate);
        var activeMode = KeywordState.Active;
        if (State == activeMode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is already {2}.", action, stateString, activeMode.Value);

        State = KeywordState.Active;
        OnActivateKeyword();
    }

    public void Deactivate()
    {
        var action = nameof(Deactivate);
        var inactiveMode = KeywordState.Inactive;
        if (State == inactiveMode)
            throw new InvalidEntityException("Cannot call action {0}, because the {1} is already {2}.", action, stateString, inactiveMode.Value);

        State = KeywordState.Inactive;
        OnDeactivateKeyword();
    }

    private void OnCreateKeyword()
    => AddEvent(new KeywordCreated(Code.Value, Title.Value));

    private void OnChangeKeywordTitle()
    => AddEvent(new KeywordTitleChanged(Code.Value, Title.Value));

    private void OnActivateKeyword()
       => AddEvent(new KeywordActivated(Code.Value, Title.Value));

    private void OnDeactivateKeyword()
    => AddEvent(new KeywordDeactivated(Code.Value, Title.Value));

    #endregion
}