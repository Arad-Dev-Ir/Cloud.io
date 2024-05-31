namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Core;
using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class ActivateKeywordCommandHandler : CommandHandler<ActivateKeyword>
{
    private readonly IKeywordCommandRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateKeywordCommandHandler(IKeywordCommandRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    private void Initialize()
    { }

    public override async Task<CommandResponse> ExecuteAsync(ActivateKeyword command)
    {
        var result = Response;
        var id = command.Id;
        var keyword = await _repo.GetGraphAsync(id);
        await keyword.IsNull(
        async () =>
        {
            AddMessage($"There is not any entity with Id: {id}.");
            result = await SetResponseAsync(ServiceStatus.NotFound);
        },
        async () =>
        {
            keyword.Activate();
            await _unitOfWork.SaveAsync();
            result = await OkAsync();
        });
        return result;
    }
}
