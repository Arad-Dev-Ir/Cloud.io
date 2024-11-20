namespace Cloudio.Core.Models;

public class Output
{
    public Error Error { get; }

    public bool IsSuccess { get; private set; }

    protected Output(bool isSuccess, Error error)
    {
        var condition1 = isSuccess == true && error != Error.None;
        var condition2 = isSuccess == false && error == Error.None;

        if (condition1 || condition2)
            throw new ArgumentException("Invalid error", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    protected Output()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    protected Output(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static Output Succeed()
    => new(true, Error.None);

    public static Output<T> Success<T>(T value)
    {
        var result = new Output<T>(value, true, Error.None);
        return result;
    }

    public static Output Fail(Error error)
    {
        var result = error;
        return result;
    }

    public static Output<T> Failure<T>(Error error)
    {
        var result = new Output<T>(default!, true, error);
        return result;
    }

    public static implicit operator Output(Error error)
    {
        var result = new Output(error);
        return result;
    }
}

public class Output<T>(T? value, bool isSuccess, Error error) : Output(isSuccess, error)
{
    private readonly T? _value = value;

    public T Value
    => IsSuccess ? _value! : throw new InvalidOperationException("The value of the failure result can't be accessed");

    public static implicit operator Output<T>(T value)
    {
        var result = value is not null ? Success<T>(value) : Failure<T>(Error.NullValue);
        return result;
    }
}


////public readonly record struct Output<T>
////{
////    private readonly T? _value = default;
////    public T Value
////    {
////        get
////        {
////            if (IsError)
////                throw new InvalidOperationException("The Value property cannot be accessed when errors have been recorded. Check IsError before accessing Value");

////            return _value!;
////        }
////    }

////    private readonly List<Error>? _errors;
////    public List<Error> Errors => _errors!;

////    public bool IsError => _errors is { Count: > 0 };

////    private Output(T value)
////    => _value = value;

////    private Output(Error error)
////    => _errors = [error];

////    private Output(List<Error> errors)
////    {
////        ArgumentNullException.ThrowIfNull(errors);

////        if (errors.Count == 0)
////            throw new ArgumentException($"Cannot create an Output<{typeof(T)}> from an empty collection of errors. Provide at least one error");

////        _errors = [.. errors];
////    }


////    public static Output<T> Success(T value)
////    => new(value);

////    public static Output<T> Failure(Error error)
////    => new(error);

////    public static Output<T> Failure(List<Error> errors)
////    => new(errors);

////    #region Operations

////    public static implicit operator Output<T>(T value)
////    => new(value);

////    public static implicit operator Output<T>(Error error)
////    => new(error);

////    public static implicit operator Output<T>(List<Error> errors)
////    => new(errors);

////    #endregion
////}

//public class Output
//{
//    protected Output(bool isSuccess, Error error)
//    {
//        var condition1 = isSuccess == true && error != Error.None;
//        var condition2 = isSuccess == false && error == Error.None;

//        if (condition1 || condition2)
//            throw new ArgumentException("Invalid error", nameof(error));

//        IsSuccess = isSuccess;
//        Error = error;
//    }

//    protected Output()
//    {
//        IsSuccess = true;
//        Error = Error.None;
//    }

//    protected Output(Error error)
//    {
//        Error = error;
//        IsSuccess = false;
//    }

//    public Error Error { get; private set; }

//    public bool IsSuccess { get; private set; }
//    public bool IsFailure => !IsSuccess;

//    public static Output Succeed()
//    => new(true, Error.None);

//    public static Output<T> Success<T>(T value)
//    => new(value, false, Error.None);

//    public static Output Fail(Error error)
//    => error;

//    public static Output<T> Failure<T>(Error error)
//    => new(default!, true, error);


//    public static implicit operator Output(Error error)
//    => new(error);
//}

//public class Output<T>(T? value, bool isSuccess, Error error) : Output(isSuccess, error)
//{
//    private readonly T? _value = value;

//    public T Value
//    => IsSuccess ? _value! : throw new InvalidOperationException("The value of the failure result can't be accessed");

//    public static implicit operator Output<T>(T value)
//    => value is not null ? Success(value) : Failure<T>(Error.Failure());
//}