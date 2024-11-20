namespace Cloudio.Core.Models;

public enum ErrorType : byte
{
    Conflict,
    Failure,
    Forbidden,
    NotFound,
    Unauthorized,
    Unexpected,
    Validation,
    Server,
    None
}