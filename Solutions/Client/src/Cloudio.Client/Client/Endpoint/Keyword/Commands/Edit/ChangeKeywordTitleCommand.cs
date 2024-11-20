namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Models;

public sealed record ChangeKeywordTitleCommand(long Id, string Title) : DataTransferObject;