namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Models;

public sealed record ActivateKeywordCommand(long Id) : DataTransferObject;