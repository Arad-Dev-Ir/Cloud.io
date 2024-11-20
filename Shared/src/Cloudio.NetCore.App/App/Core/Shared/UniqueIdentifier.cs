namespace Cloudio.Core;

/// <summary>
/// It is used to return a Guid.
/// </summary>
public static class UniqueIdentifier
{
    /// <summary>
    /// Returns a Guid.
    /// </summary>
    public static Guid GetId() => Guid.NewGuid();

    /// <summary>
    /// Returns a 'Guid' as string.
    /// </summary>
    public static string GetIdAsString() => GetId().ToString();
}