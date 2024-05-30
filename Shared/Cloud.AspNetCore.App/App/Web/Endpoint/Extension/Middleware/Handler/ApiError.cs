namespace Cloud.Web.Endpoint.API;

using Cloud.Core.Models;

public class ApiError : Model
{
    public string Id { get; set; } = Empty;
    public short Status { get; set; }
    public string Code { get; set; } = Empty;
    public string Links { get; set; } = Empty;
    public string Title { get; set; } = Empty;
    public string Detail { get; set; } = Empty;
}