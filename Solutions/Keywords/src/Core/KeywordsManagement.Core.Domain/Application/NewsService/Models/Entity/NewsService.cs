namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Web.Core;

public class NewsService : Module
{
    public Title Title { get; private set; }
    public Name Name { get; private set; }

    #region Initialize

    private NewsService(Title title, Name name)
    => Initialize(title, name, () => OnCreateService());

    private void Initialize(Title title, Name name, Action? act = default)
    {
        Title = title;
        Name = name;

        Initialize(act);
    }
    private void Initialize(Action? act = default)
    => act?.Invoke();

    public static NewsService Instance(Title title, Name name)
    => new(title, name);
    public static NewsService Instance(string title, string name)
    => new(title, name);

    #endregion

    #region Methods

    private void OnCreateService()
    => AddEvent(new ServiceCreated(Code, Title, Name));

    #endregion
}