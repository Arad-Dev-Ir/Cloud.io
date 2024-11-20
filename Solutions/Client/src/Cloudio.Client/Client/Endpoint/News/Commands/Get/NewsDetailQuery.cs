namespace Cloudio.Client.Endpoints.News;

using Microsoft.AspNetCore.Mvc;

public sealed record NewsDetailQuery([FromRoute(Name = "id")] long Id);