namespace NewsManagement.Core.News.Contracts;

using Cloudio.Core.Models;
using Cloudio.Web.Core.Contract;

public sealed record RegisterNewsCommand : DataTransferObject, IRequest<RegisterNewsCommandResponse>
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Body { get; init; }

    public required IEnumerable<string> KeywordsCodes { get; init; }
}