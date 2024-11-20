namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Models;

public sealed record TitleAndStateSearchQueryResponse(long Id, Guid Code, string Title, string State)
    : DataTransferObject;