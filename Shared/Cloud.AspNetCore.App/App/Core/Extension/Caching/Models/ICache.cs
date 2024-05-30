namespace Cloud.Core.Extensions.Caching;

public interface ICache
{
    void Add<T>(string key, T value, DateTime? absoluteExpiration, TimeSpan? slidingExpiration);
    T Get<T>(string key);
    void Remove(string key);
}