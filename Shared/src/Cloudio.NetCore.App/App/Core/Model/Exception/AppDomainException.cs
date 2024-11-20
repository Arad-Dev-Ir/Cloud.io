namespace Cloudio.Core.Models;

public class AppDomainException(string message, Dictionary<string, object?>? metadata = default) : AppException(message)
{
    public Dictionary<string, object?>? Metadata { get; } = metadata;
}