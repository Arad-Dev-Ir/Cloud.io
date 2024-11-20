namespace KeywordsManagement.Core.NewsService.Models;

using Cloudio.Web.Core;

public sealed class NewsService : Module
{
    public NewsServiceTitle Title { get; private set; }
    public NewsServiceName Name { get; private set; }

    #region Object Instantiation

    private NewsService(NewsServiceTitle title, NewsServiceName name)
    {
        Title = title;
        Name = name;

        OnNewsServiceCreated();
    }

    public static NewsService CreateInstance(NewsServiceTitle title, NewsServiceName name)
    => new(title, name);
    public static NewsService CreateInstance(string title, string name)
    => new(title, name);

    #endregion

    #region Methods

    private void OnNewsServiceCreated()
    => AddEvent(new ServiceCreated(Code.Value, Title.Value, Name.Value));

    #endregion
}