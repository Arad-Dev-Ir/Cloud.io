namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Models;

public record CreateKeywordCommand(string Title) : DataTransferObject;