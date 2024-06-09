namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using Models;

public sealed class RegisterNewsCommandHandler(INewsCommandRepository repo, IUnitOfWork unitOfWork) : CommandHandler<RegisterNews, long>
{
    private readonly INewsCommandRepository _repo = repo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public override async Task<CommandResponse<long>> ExecuteAsync(RegisterNews command, CancellationToken cancellationToken)
    {
        var keywordsCodes = command.KeywordsCodes
        .Select(Keyword.Instance)
        .ToList();

        var news = News.Instance(command.Title, command.Description, command.Body, keywordsCodes);
        _repo.Add(news);
        await _unitOfWork.SaveAsync(cancellationToken);

        var result = await OkAsync(news.Id.Value);
        return result;
    }
}
