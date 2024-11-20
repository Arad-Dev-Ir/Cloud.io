namespace Cloudio.Core.Services.Caching;

public class FakeCache : ICache
{
    public const string Key = "FackeCache";

    public FakeCache()
    { }

    public Task<T?> GetAsync<T>(string key) where T : class
    => Task.FromResult(default(T));

    public Task SetAsync<T>(string key, T value) where T : class
    => Task.CompletedTask;

    public Task<bool> DeleteAsync(string key)
    => Task.FromResult(default(bool));

    public Task DisposeAsync()
    => Task.CompletedTask;
}