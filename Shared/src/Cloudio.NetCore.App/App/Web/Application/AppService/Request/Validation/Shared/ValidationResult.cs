namespace Cloudio.Web.Core.AppService;

public class ValidationResult
{
    public Dictionary<string, object?> Errors { get; set; } = null!;

    public bool IsValid => Errors.Count == 0;
}