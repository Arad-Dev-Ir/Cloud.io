namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public sealed class DeactivateKeywordCommandHandler(IKeywordCommandRepository repo, IUnitOfWork unitOfWork) : CommandHandler<DeactivateKeyword>
{
    private readonly IKeywordCommandRepository _repo = repo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public override async Task<CommandResponse> ExecuteAsync(DeactivateKeyword command, CancellationToken cancellationToken)
    {
        CommandResponse? result;
        var id = command.Id;
        var keyword = await _repo.GetGraphAsync(id, cancellationToken);
        if (keyword is not null)
        {
            keyword.Deactivate();
            await _unitOfWork.SaveAsync(cancellationToken);
            result = await OkAsync();
        }
        else
        {
            AddMessage($"There is not any entity with Id: {id}.");
            result = await SetResponseAsync(ServiceStatus.NotFound);
        }
        return result;
    }
}
