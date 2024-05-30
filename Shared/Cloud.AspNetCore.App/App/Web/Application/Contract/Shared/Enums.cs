namespace Cloud.Web.Core.Contract;

public enum ServiceStatus
{
    Ok = 1,
    NotFound = 2,
    ValidationError = 3,
    InvalidDomainState = 4,
    Exception = 5,
    NoService = 6,
    DatabaseError = 7
}

public enum DataStatus
{
    Commit = 1,
    Rollback = 2,
}