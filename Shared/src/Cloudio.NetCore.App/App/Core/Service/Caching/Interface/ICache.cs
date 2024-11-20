namespace Cloudio.Core.Services.Caching;

public interface ICache
{
    Task<TOutput?> GetAsync<TOutput>(string key) where TOutput : class;

    Task<List<TOutput>> GetAllAsync<TOutput>(string? pattern = "*") where TOutput : class
    => Task.FromResult(new List<TOutput>());

    Task SetAsync<TInput>(string key, TInput value) where TInput : class;

    Task<bool> DeleteAsync(string key);

    Task DisposeAsync() => Task.CompletedTask;
}