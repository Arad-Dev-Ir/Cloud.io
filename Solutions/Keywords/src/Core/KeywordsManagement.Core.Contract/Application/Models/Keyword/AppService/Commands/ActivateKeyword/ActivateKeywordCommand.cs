namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public sealed record ActivateKeywordCommand(long Id) : DataTransferObject, IRequest;