namespace KeywordsManagement.Core.NewsService.Contracts;

using Cloud.Web.Core.Contract;

public class CreateNewsService : Command<long>
{
    public string Title { get; set; } = Empty;
    public string Name { get; set; } = Empty;
}