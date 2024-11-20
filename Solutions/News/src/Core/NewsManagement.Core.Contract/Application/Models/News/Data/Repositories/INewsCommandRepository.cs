namespace NewsManagement.Core.News.Contracts;

using System.Linq.Expressions;
using Cloudio.Web.Core.Contract;
using NewsManagement.Core.News.Models;

public interface INewsCommandRepository : ICommandRepository<News>
{
    Task<bool> ExistsAsync(Expression<Func<News, bool>> predicate, CancellationToken token);

    void Add(News entity);
}