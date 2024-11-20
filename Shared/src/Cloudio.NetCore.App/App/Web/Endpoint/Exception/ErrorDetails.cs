namespace Cloudio.Web.Endpoint.API;

using Microsoft.AspNetCore.Mvc;

public class ErrorDetails : ProblemDetails
{
    public string Id { get; set; } = null!;
}