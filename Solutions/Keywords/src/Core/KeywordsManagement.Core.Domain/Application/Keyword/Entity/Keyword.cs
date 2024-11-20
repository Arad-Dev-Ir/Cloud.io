namespace KeywordsManagement.Core.Keyword.Models;

using Cloudio.Core.Models;
using Cloudio.Web.Core;

public sealed class Keyword : Module
{
    public KeywordTitle Title { get; private set; }
    public KeywordState State { get; private set; }

    #region Object Instantiation

    private Keyword()
    { }
    private Keyword(KeywordTitle title)
    {
        Title = title;
        State = KeywordState.Preview;
        OnKeywordCreated();
    }

    public static Keyword CreateInstance(KeywordTitle title)
    => new(title);
    public static Keyword CreateInstance(string title)
    => new(title);

    #endregion

    #region Methods

    public void ChangeTitle(KeywordTitle title)
    {
        if (State == KeywordState.Inactive)
            throw new ChangeKeywordTitleException();

        Title = title;
        State = KeywordState.Preview;
        OnKeywordTitleChanged();
    }

    public void Activate()
    {
        if (State == KeywordState.Active)
            throw new KeywordIsAlreadyActiveException();

        State = KeywordState.Active;
        OnKeywordActivated();
    }

    public void Deactivate()
    {
        if (State == KeywordState.Inactive)
            throw new KeywordIsAlreadyInactiveException();

        State = KeywordState.Inactive;
        OnKeywordDeactivated();
    }

    private void OnKeywordCreated()
    => AddEvent(new KeywordCreated(Code.Value, Title.Value));

    private void OnKeywordTitleChanged()
    => AddEvent(new KeywordTitleChanged(Code.Value, Title.Value));

    private void OnKeywordActivated()
       => AddEvent(new KeywordActivated(Code.Value, Title.Value));

    private void OnKeywordDeactivated()
    => AddEvent(new KeywordDeactivated(Code.Value, Title.Value));

    #endregion
}