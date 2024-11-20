namespace KeywordsManagement.Core.NewsService.AppServices;

using Cloudio.Web.Core.Contract;
using KeywordsManagement.Core.NewsService.Contracts;
using KeywordsManagement.Core.NewsService.Models;

public sealed class CreateNewsServiceCommandHandler(INewsServiceCommandRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateNewsServiceCommand, CreateNewsServiceCommandResponse>
{
    private readonly INewsServiceCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateNewsServiceCommandResponse> HandleAsync(CreateNewsServiceCommand input, CancellationToken token)
    {
        var newsServiceName = NewsServiceName.CreateInstance(input.Name);

        if (await _repository.ExistsAsync(e => e.Name == newsServiceName, token))
            throw new NewsServiceAlreadyExistsException(newsServiceName.Value);

        var service = NewsService.CreateInstance(input.Title, input.Name);
        _repository.Add(service);
        await _unitOfWork.SaveAsync(token);

        var result = new CreateNewsServiceCommandResponse(service.Id.Value);
        return result;
    }
}