namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Models;

public sealed record DeactivateKeywordCommand(long Id) : DataTransferObject;