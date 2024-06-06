namespace KeywordsManagement.Core.NewsService.Contracts;

using Models;

public interface INewsServiceCommandRepository
{
    Task AddAsync(NewsService entity, CancellationToken cancellationToken);
}