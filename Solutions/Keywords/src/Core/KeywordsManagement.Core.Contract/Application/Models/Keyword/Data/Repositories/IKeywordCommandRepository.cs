namespace KeywordsManagement.Core.Keyword.Contracts;

using System.Linq.Expressions;
using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.Keyword.Models;

public interface IKeywordCommandRepository : ICommandRepository<Keyword>
{
    Task<Keyword?> GetGraphAsync(Id id, CancellationToken token);

    Task AddAsync(Keyword entity, CancellationToken token);

    Task<bool> ExistsAsync(Expression<Func<Keyword, bool>> predicate, CancellationToken token);

    void Add(Keyword entity);
}