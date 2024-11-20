namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public record CreateKeywordCommand(string Title) : DataTransferObject, IRequest<CreateKeywordCommandResponse>;