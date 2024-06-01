namespace NewsManagement.Core.News.Contracts;

using News = Models.News;

public interface INewsCommandRepository
{
    Task AddAsync(News entity);
    void Add(News entity);
}