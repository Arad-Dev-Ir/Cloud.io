namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public sealed record RegisterNews(string Title, string Description, string Body, IEnumerable<string> KeywordsCodes) : Command<long>;