namespace NewsManagement.Data.External.Messaging;

using Cloudio.Core.Models;

public sealed record Keyword(Guid Code, string Title) : Event;