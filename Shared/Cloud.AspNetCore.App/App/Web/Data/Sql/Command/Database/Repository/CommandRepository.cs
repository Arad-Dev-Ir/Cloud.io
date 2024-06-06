namespace Cloud.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Cloud.Core.Models;
using Web.Core.Contract;
using Web.Core;

public class CommandRepository<C, M> : ICommandRepository<M> where C : CommandContext where M : Module
{
    public CommandRepository(C context) => Context = context;
    protected readonly C Context;

    public M Get(Id id)
    {
        var collection = GetCollection();
        var result = collection.Find(id);
        return result;
    }

    public M Get(Code code)
    {
        var collection = GetCollection();
        var result = collection.FirstOrDefault(e => e.Code == code);
        return result;
    }

    public async Task<M> GetAsync(Id id, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        var result = await collection.FindAsync(id, cancellationToken);
        return result;
    }

    public async Task<M> GetAsync(Code code, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        var result = await collection.FirstOrDefaultAsync(e => e.Code == code, cancellationToken);
        return result;
    }

    public M GetGraph(Id id)
    {
        var collection = GetCollection();
        var query = collection.AsQueryable();
        var navigations = Context.GetInclude(GetEntityType());
        foreach (var item in navigations) query.Include(item);
        var result = query.FirstOrDefault(e => e.Id == id);
        return result;
    }

    public async Task<M> GetGraphAsync(Id id, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        var query = collection.AsQueryable();
        var navigations = Context.GetInclude(GetEntityType());
        foreach (var item in navigations) query.Include(item);
        var result = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        return result;
    }

    public M GetGraph(Code code)
    {
        var collection = GetCollection();
        var query = collection.AsQueryable();
        var navigations = Context.GetInclude(GetEntityType());
        foreach (var item in navigations) query.Include(item);
        var result = query.FirstOrDefault(e => e.Code == code);
        return result;
    }

    public async Task<M> GetGraphAsync(Code code, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        var query = collection.AsQueryable();
        var navigations = Context.GetInclude(GetEntityType());
        foreach (var item in navigations) query.Include(item);
        var result = await query.FirstOrDefaultAsync(e => e.Code == code, cancellationToken);
        return result;
    }


    public void Add(M entity)
    => GetCollection().Add(entity);

    public async Task AddAsync(M entity, CancellationToken cancellationToken)
    => await GetCollection().AddAsync(entity, cancellationToken);


    public void Delete(Id id)
    {
        var collection = GetCollection();
        var entity = collection.Find(id);
        collection.Remove(entity);
    }

    public void DeleteGraph(Id id)
    {
        var collection = GetCollection();
        var query = collection.AsQueryable();
        var navigations = Context.GetInclude(GetEntityType());
        foreach (var item in navigations) query.Include(item);
        var entity = query.FirstOrDefault(e => e.Id == id);
        if (entity?.Id.Value > 0) Delete(entity);
    }

    public void Delete(M entity)
    => GetCollection().Remove(entity);


    public bool Exists(Expression<Func<M, bool>> predicate)
    => GetCollection().Any(predicate);

    public async Task<bool> ExistsAsync(Expression<Func<M, bool>> predicate, CancellationToken cancellationToken)
    => await GetCollection().AnyAsync(predicate, cancellationToken);

    private DbSet<M> GetCollection()
    => Context.Set<M>();

    private Type GetEntityType()
    => typeof(M);
}
