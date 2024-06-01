namespace KeywordsManagement.Core.NewsService.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using Service = Models.NewsService;

public class CreateNewsServiceCommandHandler : CommandHandler<CreateNewsService, long>
{
    private readonly INewsServiceCommandRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNewsServiceCommandHandler(INewsServiceCommandRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    private void Initialize()
    { }

    public override async Task<CommandResponse<long>> ExecuteAsync(CreateNewsService command)
    {
        var result = default(CommandResponse<long>);
        var service = Service.Instance(command.Title, command.Name);
        await _repo.AddAsync(service);
        await _unitOfWork.SaveAsync();
        result = await OkAsync(service.Id.Value);
        return result;
    }
}