namespace KeywordsManagement.Core.Keyword.Contracts;

using Cloudio.Core.Models;

public sealed record CreateKeywordCommandResponse(long Id) : DataTransferObject;