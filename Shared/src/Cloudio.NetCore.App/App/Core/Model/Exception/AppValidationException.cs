namespace Cloudio.Core.Models;

public class AppValidationException(Dictionary<string, object?> metadata) : AppDomainException(Note, metadata)
{
    private const string Note = "One or more validation errors occurred";
}