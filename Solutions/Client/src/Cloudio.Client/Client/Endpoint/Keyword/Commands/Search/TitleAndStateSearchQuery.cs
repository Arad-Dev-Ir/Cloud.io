namespace Cloudio.Client.Endpoints.Keywords;

public sealed record TitleAndStateSearchQuery(string Title, string? State = default) : PageQuery;