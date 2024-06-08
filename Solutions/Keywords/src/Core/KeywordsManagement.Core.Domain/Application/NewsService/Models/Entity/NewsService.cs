namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Web.Core;

public class NewsService : Module
{
    public Title Title { get; private set; }
    public Name Name { get; private set; }

    #region Initialize

    private NewsService(Title title, Name name)
    {
        Title = title;
        Name = name;

        OnCreateService();
    }

    public static NewsService Instance(Title title, Name name)
    => new(title, name);
    public static NewsService Instance(string title, string name)
    => new(title, name);

    #endregion

    #region Methods

    private void OnCreateService()
    => AddEvent(new ServiceCreated(Code.Value, Title.Value, Name.Value));

    #endregion
}