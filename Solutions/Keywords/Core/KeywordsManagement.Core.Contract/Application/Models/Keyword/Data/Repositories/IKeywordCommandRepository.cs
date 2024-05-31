namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloud.Core.Models;
using Keyword = Models.Keyword;

public interface IKeywordCommandRepository
{
    Task<Keyword> GetGraphAsync(Id id);
    Task AddAsync(Keyword entity);
    void Add(Keyword entity);
}