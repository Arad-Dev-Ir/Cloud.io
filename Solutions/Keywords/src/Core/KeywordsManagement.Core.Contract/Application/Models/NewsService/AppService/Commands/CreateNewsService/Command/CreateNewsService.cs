namespace KeywordsManagement.Core.NewsService.Contracts;

using Cloud.Web.Core.Contract;

public record CreateNewsService(string Title, string Name) : Command<long>;