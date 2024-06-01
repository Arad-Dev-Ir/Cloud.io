namespace NewsManagement.Core.News.AppServices;

using Cloud.Web.Core.AppService;
using Cloud.Web.Core.Contract;
using Contracts;
using News = Models.News;
using Keyword = Models.Keyword;

public class RegisterNewsCommandHandler : CommandHandler<RegisterNews, long>
{
    private readonly INewsCommandRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterNewsCommandHandler(INewsCommandRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public void Initialize()
    { }

    public override async Task<CommandResponse<long>> ExecuteAsync(RegisterNews command)
    {
        var result = default(CommandResponse<long>);

        var keywordsCodes = command.KeywordsCodes
        .Select(e => Keyword.Instance(e))
        .ToList();

        var news = News.Instance(command.Title, command.Description, command.Body, keywordsCodes);
        _repo.Add(news);
        await _unitOfWork.SaveAsync();

        result = await OkAsync(news.Id.Value);
        return result;
    }
}
