namespace Cloud.Core.Extensions.Lifetime;

public interface ILifetime
{ }

public interface ISingleton : ILifetime
{ }

public interface IScoped : ILifetime
{ }

public interface ITransient : ILifetime
{ }