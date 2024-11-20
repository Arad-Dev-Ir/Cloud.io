namespace Cloudio.Web.Data.Sql.Command;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Cloudio.Core.Models;
using Cloudio.Core.Services.Identity;
using Cloudio.Web.Core;

public static partial class Extensions
{
    public static void SetConvension<T, C>(this ModelConfigurationBuilder builder) where C : IConversion
    => builder.Properties<T>().HaveConversion<C>();

    internal static void SetEntityProperties(this ModelBuilder builder)
    {
        var entityTypes = builder.GetEntityTypes<Entity>();
        foreach (var item in entityTypes)
        {
            var clrType = item.ClrType;
            builder.SetEntityIdentityProperty(clrType);
            builder.SetEntityShadowProperty(clrType);
            //builder.SetVersionShadowProperty(item);
        }
    }

    internal static void SetEntityIdentityProperty(this ModelBuilder builder, Type clrType)
    {
        builder.Entity(clrType).HasKey(nameof(Id));

        builder.Entity(clrType).Property(nameof(Id))
        .UseIdentityColumn(1, 1)
        .ValueGeneratedOnAdd();
    }

    internal static void SetEntityShadowProperty(this ModelBuilder builder, Type clrType)
    {
        builder.Entity(clrType).Property<string>(CreatedByUserId).HasMaxLength(50);
        builder.Entity(clrType).Property<string>(ModifiedByUserId).HasMaxLength(50);
        builder.Entity(clrType).Property<DateTime?>(CreatedDateTime);
        builder.Entity(clrType).Property<DateTime?>(ModifiedDateTime);
    }

    internal static void SetEntitiesShadowPropertyValues(this ChangeTracker changeTracker, IUserIdentity userInfo)
    {
        var userId = userInfo.GetUserId();
        var time = DateTime.UtcNow;

        var addedEntries = changeTracker.GetEntries<Entity>(EntityState.Added);
        foreach (var item in addedEntries)
        {
            item.Property(CreatedByUserId).CurrentValue = userId;
            item.Property(CreatedDateTime).CurrentValue = time;
        }

        var modifiedEntries = changeTracker.GetEntries<Entity>(EntityState.Modified);
        foreach (var item in modifiedEntries)
        {
            item.Property(ModifiedByUserId).CurrentValue = userId;
            item.Property(ModifiedDateTime).CurrentValue = time;
        }
    }

    internal static void SetVersionShadowProperty(this ModelBuilder builder, IMutableEntityType entityType)
    {
        builder
        .Entity(entityType.ClrType)
        .Property<byte[]>(Version)
        .IsRowVersion();
    }

    internal static List<Module> GetModules(this ChangeTracker changeTracker)
    {
        var result = changeTracker
        .Entries<Module>()
        .Select(e => e.Entity)
        .ToList();
        return result;
    }

    internal static M GetModule<M>(this ChangeTracker changeTracker) where M : Entity
    {
        var result = changeTracker
        .Entries<M>()
        .Select(e => e.Entity)
        .Single();
        return result;
    }

    internal static List<Module> GetChangedModules(this ChangeTracker changeTracker)
    {
        var result = changeTracker
        .GetEntries(IsChanged<Module>())
        .Select(e => e.Entity)
        .ToList();
        return result;
    }

    internal static List<Module> GetModulesWithEvents(this ChangeTracker changeTracker)
    {
        var result = changeTracker
        .GetEntries(IsNotDetached<Module>())
        .Select(e => e.Entity)
        .Where(e => e.Events.Count > 0)
        .ToList();
        return result;
    }

    #region Private Methods

    private static List<IMutableEntityType> GetEntityTypes<T>(this ModelBuilder builder) where T : Entity
    {
        var result = builder
        .Model
        .GetEntityTypes()
        .Where(e => typeof(T).IsAssignableFrom(e.ClrType))
        .ToList();
        return result;
    }

    private static List<EntityEntry<T>> GetEntries<T>(this ChangeTracker changeTracker, EntityState state) where T : Entity
    {
        var result = changeTracker
        .GetEntries<T>(e => e.State == state)
        .ToList();
        return result;
    }

    private static List<EntityEntry<T>> GetEntries<T>(this ChangeTracker changeTracker, Func<EntityEntry<T>, bool> predicate) where T : Entity
    {
        var result = changeTracker
        .Entries<T>()
        .Where(predicate)
        .ToList();
        return result;
    }

    private static Func<EntityEntry<T>, bool> IsChanged<T>() where T : Entity
    => (e) => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified;

    private static Func<EntityEntry<T>, bool> IsNotDetached<T>() where T : Entity
    => (e) => e.State != EntityState.Detached;

    #endregion
}

public static partial class Extensions
{
    internal static readonly string CreatedByUserId = nameof(CreatedByUserId);
    internal static readonly Func<object, string> GetCreatedByUserIdProperty = (e) => GetProperty<string>(e, CreatedByUserId);

    internal static readonly string ModifiedByUserId = nameof(ModifiedByUserId);
    internal static readonly Func<object, string> GetModifiedByUserIdProperty = (e) => GetProperty<string>(e, ModifiedByUserId);

    internal static readonly string CreatedDateTime = nameof(CreatedDateTime);
    internal static readonly Func<object, DateTime?> GetCreatedDateTimeProperty = (e) => GetProperty<DateTime?>(e, CreatedDateTime);

    internal static readonly string ModifiedDateTime = nameof(ModifiedDateTime);
    internal static readonly Func<object, DateTime?> GetModifiedDateTimeProperty = (e) => GetProperty<DateTime?>(e, ModifiedDateTime);

    internal readonly static string Version = nameof(Version);

    internal static T GetProperty<T>(object entity, string name)
    => EF.Property<T>(entity, name);
}
