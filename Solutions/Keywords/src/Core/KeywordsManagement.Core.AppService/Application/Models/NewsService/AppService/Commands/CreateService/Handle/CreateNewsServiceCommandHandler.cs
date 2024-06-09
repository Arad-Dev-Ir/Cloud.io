namespace KeywordsManagement.Core.NewsService.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using Models;

public class CreateNewsServiceCommandHandler(INewsServiceCommandRepository repo, IUnitOfWork unitOfWork) : CommandHandler<CreateNewsService, long>
{
    private readonly INewsServiceCommandRepository _repo = repo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public override async Task<CommandResponse<long>> ExecuteAsync(CreateNewsService command, CancellationToken cancellationToken)
    {
        var service = NewsService.Instance(command.Title, command.Name);
        await _repo.AddAsync(service, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        var result = await OkAsync(service.Id.Value);
        return result;
    }
}