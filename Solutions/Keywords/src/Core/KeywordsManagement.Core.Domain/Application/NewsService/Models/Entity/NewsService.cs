namespace KeywordsManagement.Core.NewsService.Models;

using Cloud.Web.Core;

public sealed class NewsService : Module
{
    public NewsServiceTitle Title { get; private set; }
    public NewsServiceName Name { get; private set; }

    #region Initialize

    private NewsService(NewsServiceTitle title, NewsServiceName name)
    {
        Title = title;
        Name = name;

        OnCreateService();
    }

    public static NewsService Instance(NewsServiceTitle title, NewsServiceName name)
    => new(title, name);
    public static NewsService Instance(string title, string name)
    => new(title, name);

    #endregion

    #region Methods

    private void OnCreateService()
    => AddEvent(new ServiceCreated(Code.Value, Title.Value, Name.Value));

    #endregion
}