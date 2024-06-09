namespace NewsManagement.Core.News.Contracts;

using Models;

public interface INewsCommandRepository
{
    Task AddAsync(News entity, CancellationToken cancellationToken);
    void Add(News entity);
}