namespace NewsManagement.Core.News.AppServices;

using Cloudio.Web.Core.Contract;
using NewsManagement.Core.News.Contracts;
using NewsManagement.Core.News.Models;

public sealed class RegisterNewsCommandHandler : IRequestHandler<RegisterNewsCommand, RegisterNewsCommandResponse>
{
    private readonly INewsCommandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterNewsCommandHandler(INewsCommandRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterNewsCommandResponse> HandleAsync(RegisterNewsCommand input, CancellationToken token)
    {
        var newsTitle = NewsTitle.CreateInstance(input.Title);

        if (await _repository.ExistsAsync(e => e.Title == newsTitle, token))
            throw new NewsAlreadyExistsException(newsTitle.Value);

        var keywordsCodes = input.KeywordsCodes.Select(e => Keyword.CreateInstance(e)).ToList();
        var news = News.CreateInstance(input.Title, input.Description, input.Body, keywordsCodes);
        _repository.Add(news);
        await _unitOfWork.SaveAsync(token);

        var result = new RegisterNewsCommandResponse(news.Id.Value);
        return result;
    }
}
