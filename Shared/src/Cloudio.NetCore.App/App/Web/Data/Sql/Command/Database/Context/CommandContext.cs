namespace Cloudio.Web.Data.Sql.Command;

using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Cloudio.Core.Models;

public abstract class CommandContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.SetEntityProperties();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    => base.ConfigureConventions(SetConventions(builder));

    protected virtual ModelConfigurationBuilder SetConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<string>().AreUnicode(false).HaveMaxLength(500);
        builder.Properties<decimal>().HavePrecision(18, 6);
        builder.SetConvension<Id, IdConversion>();
        builder.SetConvension<Code, CodeConversion>();
        return builder;
    }

    #region Transaction

    protected IDbContextTransaction? Transaction;
    public async Task StartAsync(CancellationToken token)
    => Transaction = await Database.BeginTransactionAsync(token);

    public async Task CommitAsync(CancellationToken token)
    {
        if (Transaction is { })
            await Transaction.CommitAsync(token);
        else
            throw new NullReferenceException("Please call 'StartAsync' method first");
    }

    public async Task RollbackAsync(CancellationToken token)
    {
        if (Transaction is { })
            await Transaction.RollbackAsync(token);
        else
            throw new NullReferenceException("Please call 'StartAsync' method first");
    }

    #endregion

    #region Save

    public async Task<int> SaveAsync(CancellationToken token)
    => await SaveChangesAsync(token);

    public int Save()
    => SaveChanges();

    #endregion

    #region Get Graph of Data

    public IEnumerable<string> GetInclude(Type type)
    {
        var entityType = Model.FindEntityType(type);
        var includedNavigations = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var entityNavigations = new List<INavigation>();
            foreach (var navigation in entityType.GetNavigations())
            {
                if (includedNavigations.Add(navigation)) entityNavigations.Add(navigation);
            }
            if (entityNavigations.Count == 0)
            {
                if (stack.Count > 0) yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
            }
            else
            {
                foreach (var navigation in entityNavigations)
                {
                    var inverseNavigation = navigation.Inverse;
                    if (inverseNavigation != null) includedNavigations.Add(inverseNavigation);
                }
                stack.Push(entityNavigations.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
                stack.Pop();
            if (stack.Count == 0) break;
            entityType = stack.Peek().Current.TargetEntityType;
        }
    }

    #endregion
}
