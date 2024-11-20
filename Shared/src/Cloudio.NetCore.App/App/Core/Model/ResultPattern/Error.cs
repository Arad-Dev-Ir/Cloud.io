namespace Cloudio.Core.Models;

public readonly record struct Error
{
    private Error(string id, string title, string detail, ErrorType type, IDictionary<string, object?>? extensions)
    {
        Id = id;
        Title = title;
        Detail = detail;
        Type = type.ToString();
        Extensions = extensions;
    }

    public string Id { get; }
    public string Title { get; }
    public string Detail { get; }
    public string Type { get; }
    public IDictionary<string, object?>? Extensions { get; }

    public static Error None
    => new(string.Empty, string.Empty, string.Empty, ErrorType.None, null);

    public static Error NullValue
    => new(UniqueIdentifier.GetIdAsString(), "Value is null", "Value is null", ErrorType.Failure, null);

    #region Instantiation Methods

    public static Error Unexpected(string id, string title = "Unexpected", string detail = "An unexpected error has occurred", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Unexpected, extensions);

    public static Error Failure(string id, string title = "Failure", string detail = "A failure has occurred", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Failure, extensions);

    public static Error Validation(string id, string title = "Validation", string detail = "A validation error has occurred", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Validation, extensions);

    public static Error Conflict(string id, string title = "Conflict", string detail = "A conflict error has occurred.", IDictionary<string, object?>? extensions = null)
     => new(id, title, detail, ErrorType.Conflict, extensions);

    public static Error NotFound(string id, string title = "NotFound", string detail = "A 'Not Found' error has occurred.", IDictionary<string, object?>? extensions = null)
     => new(id, title, detail, ErrorType.NotFound, extensions);

    public static Error Unauthorized(string id, string title = "Unauthorized", string detail = "An 'Unauthorized' error has occurred.", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Unauthorized, extensions);

    public static Error Forbidden(string id, string title = "Forbidden", string detail = "A 'Forbidden' error has occurred.", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Forbidden, extensions);

    public static Error Internal(string id, string title = "Internal", string detail = "A 'Server' error has occurred.", IDictionary<string, object?>? extensions = null)
    => new(id, title, detail, ErrorType.Forbidden, extensions);

    #endregion
}