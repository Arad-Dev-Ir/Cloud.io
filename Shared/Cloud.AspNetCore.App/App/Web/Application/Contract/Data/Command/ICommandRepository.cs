namespace Cloud.Web.Core.Contract;

using System.Linq.Expressions;
using Cloud.Core.Models;

public interface ICommandRepository<M> where M : Module
{
    M Get(Id id);
    Task<M> GetAsync(Id id);
    M Get(Code code);
    Task<M> GetAsync(Code code);

    M GetGraph(Id id);
    Task<M> GetGraphAsync(Id id);
    M GetGraph(Code code);
    Task<M> GetGraphAsync(Code code);

    void Add(M entity);
    Task AddAsync(M entity);

    void Delete(Id id);
    void DeleteGraph(Id id);
    void Delete(M entity);

    bool Exists(Expression<Func<M, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<M, bool>> predicate);
}