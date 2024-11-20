namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public sealed record DeactivateKeywordCommand(long Id) : DataTransferObject, IRequest;