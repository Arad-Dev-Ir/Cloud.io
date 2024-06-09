namespace KeywordsManagement.Core.Keyword.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;

public class ChangeKeywordTitleCommandHandler(IKeywordCommandRepository repo, IUnitOfWork unitOfWork) : CommandHandler<ChangeKeywordTitle>
{
    private readonly IKeywordCommandRepository _repo = repo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public override async Task<CommandResponse> ExecuteAsync(ChangeKeywordTitle command, CancellationToken cancellationToken)
    {
        CommandResponse? result;
        var id = command.Id;
        var keyword = await _repo.GetGraphAsync(id, cancellationToken);
        if (keyword is not null)
        {
            keyword.ChangeTitle(command.Title);
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
