namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Core;
using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class DeactivateKeywordCommandHandler : CommandHandler<DeactivateKeyword>
{
    private readonly IKeywordCommandRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateKeywordCommandHandler(IKeywordCommandRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    private void Initialize()
    { }

    public override async Task<CommandResponse> ExecuteAsync(DeactivateKeyword command, CancellationToken cancellationToken)
    {
        var result = Response;
        var id = command.Id;
        var keyword = await _repo.GetGraphAsync(id, cancellationToken);
        await keyword.IsNull(
        async () =>
        {
            AddMessage($"There is not any entity with Id: {id}.");
            result = await SetResponseAsync(ServiceStatus.NotFound);
        },
        async () =>
        {
            keyword.Deactivate();
            await _unitOfWork.SaveAsync(cancellationToken);
            result = await OkAsync();
        });
        return result;
    }
}
