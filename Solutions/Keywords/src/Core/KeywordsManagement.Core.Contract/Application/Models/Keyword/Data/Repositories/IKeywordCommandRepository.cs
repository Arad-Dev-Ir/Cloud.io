namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;
using Models;

public interface IKeywordCommandRepository
{
    Task<Keyword> GetGraphAsync(Id id, CancellationToken cancellationToken);
    Task AddAsync(Keyword entity, CancellationToken cancellationToken);
    void Add(Keyword entity);
}