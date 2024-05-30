namespace Cloud.Web.Data.Sql.Command
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Cloud.Core.Models;
    using Cloud.Core.Extensions.Identity;
    using Web.Core;

    // ChangeTracker
    public static partial class Extension
    {
        private static List<EntityEntry<T>> GetEntries<T>(this ChangeTracker source, EntityState state) where T : Entity
        {
            var result = source
            .GetEntries<T>(e => e.State == state)
            .ToList();
            return result;
        }

        private static List<EntityEntry<T>> GetEntries<T>(this ChangeTracker source, Func<EntityEntry<T>, bool> predicate) where T : Entity
        {
            var result = source
            .GetEntries<T>()
            .Where(predicate)
            .ToList();
            return result;
        }

        private static IEnumerable<EntityEntry<T>> GetEntries<T>(this ChangeTracker source) where T : Entity
        => source.Entries<T>();

        internal static List<Module> GetModules(this ChangeTracker source)
        {
            var result = source
            .GetEntries<Module>()
            .Select(e => e.Entity)
            .ToList();
            return result;
        }

        internal static M GetModule<M>(this ChangeTracker source) where M : Entity
        {
            var result = source
            .GetEntries<M>()
            .Select(e => e.Entity)
            .Single();
            return result;
        }

        internal static List<Module> GetChangedModules(this ChangeTracker source)
        {
            var result = source
            .GetEntries(IsChanged<Module>())
            .Select(e => e.Entity)
            .ToList();
            return result;
        }

        internal static List<Module> GetModulesWithEvent(this ChangeTracker source)
        {
            var result = source
            .GetEntries(IsNotDetached<Module>())
            .Select(e => e.Entity)
            .Where(e => e.GetEvents().Any())
            .ToList();
            return result;
        }

        private static Func<EntityEntry<T>, bool> IsChanged<T>() where T : Entity
        => (e) => e.State == EntityState.Added || e.State == EntityState.Deleted || e.State == EntityState.Modified;

        private static Func<EntityEntry<T>, bool> IsNotDetached<T>() where T : Entity
        => (e) => e.State != EntityState.Detached;

        internal static void SetAuditData(this ChangeTracker source, IUserIdentity userInfo)
        {
            var userId = userInfo.GetUserId();
            var time = DateTime.UtcNow;

            var addedEntries = source.GetEntries<Entity>(EntityState.Added);
            foreach (var item in addedEntries)
            {
                item.Property(CreatedByUserId).CurrentValue = userId;
                item.Property(CreatedDateTime).CurrentValue = time;
            }

            var modifiedEntries = source.GetEntries<Entity>(EntityState.Modified);
            foreach (var item in modifiedEntries)
            {
                item.Property(ModifiedByUserId).CurrentValue = userId;
                item.Property(ModifiedDateTime).CurrentValue = time;
            }
        }
    }
}

namespace Cloud.Web.Data.Sql.Command
{
    using Microsoft.EntityFrameworkCore;

    // auditable shadow property
    public static partial class Extension
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
}

namespace Cloud.Web.Data.Sql.Command
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Cloud.Core.Models;

    // Model Builder
    public static partial class Extension
    {
        private static List<IMutableEntityType> GetEntityTypes<T>(this ModelBuilder source) where T : Entity
        {
            var result = source
            .Model
            .GetEntityTypes()
            .Where(e => typeof(T).IsAssignableFrom(e.ClrType))
            .ToList();
            return result;
        }

        internal static void SetEntityProperties(this ModelBuilder source)
        {
            var entityTypes = source.GetEntityTypes<Entity>();
            foreach (var item in entityTypes)
            {
                var clrType = item.ClrType;
                source.SetIdentityKeyProperty(clrType);
                source.SetAuditableShadowProperty(clrType);
                //source.SetVersionShadowProperty(item);
            }
        }

        // Instead of this method, it's possible to use interceptors
        internal static void SetIdentityKeyProperty(this ModelBuilder source, Type clrType)
        {
            source.Entity(clrType).HasKey(nameof(Id));
            source.Entity(clrType).Property(nameof(Id)).UseIdentityColumn(1, 1);
        }

        internal static void SetAuditableShadowProperty(this ModelBuilder source, Type clrType)
        {
            source.Entity(clrType).Property<string>(CreatedByUserId).HasMaxLength(50);
            source.Entity(clrType).Property<string>(ModifiedByUserId).HasMaxLength(50);
            source.Entity(clrType).Property<DateTime?>(CreatedDateTime);
            source.Entity(clrType).Property<DateTime?>(ModifiedDateTime);
        }

        // Using version to manage concurrency
        internal static void SetVersionShadowProperty(this ModelBuilder source, IMutableEntityType entityType)
        {
            source
            .Entity(entityType.ClrType)
            .Property<byte[]>(Version)
            .IsRowVersion();
        }
    }
}

namespace Cloud.Web.Data.Sql.Command
{
    using Microsoft.EntityFrameworkCore;

    public static partial class Extension
    {
        public static void SetConvension<T, C>(this ModelConfigurationBuilder source) where C : IConversion
        => source.Properties<T>().HaveConversion<C>();
    }
}