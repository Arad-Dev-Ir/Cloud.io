namespace NewsManagement.Core.News.Contracts;

using Cloudio.Core.Models;

public sealed record RegisterNewsCommandResponse(long Id) : DataTransferObject;