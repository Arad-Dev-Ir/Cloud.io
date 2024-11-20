namespace Cloudio.Client.Endpoints.News;

using Cloudio.Core.Models;

public sealed record RegisterNewsCommand
    (string Title, string Description, string Body, IEnumerable<string> KeywordsCodes) : DataTransferObject;