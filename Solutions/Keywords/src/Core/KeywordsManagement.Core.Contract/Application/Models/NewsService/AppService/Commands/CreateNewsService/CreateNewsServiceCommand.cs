namespace KeywordsManagement.Core.NewsService.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public sealed record CreateNewsServiceCommand(string Title, string Name)
    : DataTransferObject, IRequest<CreateNewsServiceCommandResponse>;