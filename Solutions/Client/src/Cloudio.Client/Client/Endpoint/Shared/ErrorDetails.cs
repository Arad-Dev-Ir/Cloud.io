namespace Cloudio.Client.Endpoints;

public class ErrorDetails
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public int? Status { get; set; }

    public string? Detail { get; set; }

    public List<Failure>? Failures { get; set; }
}

public class Failure
{
    public string Property { get; set; } = null!;
    public List<string> Messages { get; set; } = null!;
}
