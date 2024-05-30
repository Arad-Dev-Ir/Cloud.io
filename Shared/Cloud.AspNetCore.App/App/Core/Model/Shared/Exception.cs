namespace Cloud.Core.Models;

public class AppException : CoreException
{
    public AppException(string message, params string[] parameters) : base(message, parameters)
    { }

    public AppException(string message) : base(message)
    { }
}

public class InvalidElementException : CoreException
{
    public InvalidElementException(string message, params string[] parameters) : base(message, parameters) { }

    public InvalidElementException(string message) : base(message)
    { }
}

public class InvalidEntityException : CoreException
{
    public InvalidEntityException(string message, params string[] parameters) : base(message, parameters) { }

    public InvalidEntityException(string message) : base(message)
    { }
}
