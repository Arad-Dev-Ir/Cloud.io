namespace Cloudio.Web.Data.Sql.Command;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Cloudio.Core.Models;
using Cloudio.Web.Core;

public class CommandRepository<C, M> where C : CommandContext where M : Module
{
    private readonly Type _type = typeof(M);

    public CommandRepository(C context)
    {
        Context = context;
        Set = Context.Set<M>();
    }

    protected readonly C Context;
    protected DbSet<M> Set;


    public M? Get(Id id)
    {
        var result = Set.Find(id);
        return result;
    }

    public M? Get(Code code)
    {
        var result = Set.FirstOrDefault(e => e.Code == code);
        return result;
    }

    public async Task<M?> GetAsync(Id id, CancellationToken token)
    {
        var result = await Set.FindAsync([id, token], cancellationToken: token);
        return result;
    }

    public async Task<M?> GetAsync(Code code, CancellationToken token)
    {
        var result = await Set.FirstOrDefaultAsync(e => e.Code == code, token);
        return result;
    }

    public M? GetGraph(Id id)
    {
        var query = Set.AsQueryable();
        var navigations = Context.GetInclude(_type);
        foreach (var item in navigations) query.Include(item);

        var result = query.FirstOrDefault(e => e.Id == id);
        return result;
    }

    public async Task<M?> GetGraphAsync(Id id, CancellationToken token)
    {
        var query = Set.AsQueryable();
        var navigations = Context.GetInclude(_type);
        foreach (var item in navigations) query.Include(item);

        var result = await query.FirstOrDefaultAsync(e => e.Id == id, token);
        return result;
    }

    public M? GetGraph(Code code)
    {
        var query = Set.AsQueryable();
        var navigations = Context.GetInclude(_type);
        foreach (var item in navigations) query.Include(item);

        var result = query.FirstOrDefault(e => e.Code == code);
        return result;
    }

    public async Task<M?> GetGraphAsync(Code code, CancellationToken token)
    {
        var query = Set.AsQueryable();
        var navigations = Context.GetInclude(_type);
        foreach (var item in navigations) query.Include(item);

        var result = await query.FirstOrDefaultAsync(e => e.Code == code, token);
        return result;
    }

    public void Add(M entity)
    {
        Set.Add(entity);
    }

    public async Task AddAsync(M entity, CancellationToken token)
    => await Set.AddAsync(entity, token);


    public async Task Delete(Id id, CancellationToken token)
    {
        var entity = await GetAsync(id, token);
        if (entity is { })
            Set.Remove(entity);
    }

    public async Task DeleteGraph(Id id, CancellationToken token)
    {
        var query = Set.AsQueryable();
        var navigations = Context.GetInclude(_type);
        foreach (var item in navigations) query.Include(item);

        var entity = await query.FirstOrDefaultAsync(e => e.Id == id, token);
        if (entity is { })
            Delete(entity);
    }

    public void Delete(M entity)
    => Set.Remove(entity);

    public bool Exists(Expression<Func<M, bool>> predicate)
    {
        var result = Set.Any(predicate);
        return result;
    }

    public async Task<bool> ExistsAsync(Expression<Func<M, bool>> predicate, CancellationToken token)
    {
        var result = await Set.AnyAsync(predicate, token);
        return result;
    }
}
