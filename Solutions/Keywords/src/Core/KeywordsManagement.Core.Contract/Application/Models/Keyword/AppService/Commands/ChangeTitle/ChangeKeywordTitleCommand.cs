namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public sealed record ChangeKeywordTitleCommand(long Id, string Title) : DataTransferObject, IRequest;