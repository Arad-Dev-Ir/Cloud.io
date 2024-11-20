namespace KeywordsManagement.Core.NewsService.Contracts;

using Cloudio.Core.Models;

public sealed record CreateNewsServiceCommandResponse(long Id) : DataTransferObject;
