namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;
using Keyword = Models.Keyword;

public interface IKeywordCommandRepository
{
    Task<Keyword> GetGraphAsync(Id id, CancellationToken cancellationToken);
    Task AddAsync(Keyword entity, CancellationToken cancellationToken);
    void Add(Keyword entity);
}