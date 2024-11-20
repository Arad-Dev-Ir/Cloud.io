namespace KeywordsManagement.Core.NewsService.Contracts;

using System.Linq.Expressions;
using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.NewsService.Models;

public interface INewsServiceCommandRepository : ICommandRepository<NewsService>
{
    void Add(NewsService entity);

    Task<bool> ExistsAsync(Expression<Func<NewsService, bool>> predicate, CancellationToken token);
}