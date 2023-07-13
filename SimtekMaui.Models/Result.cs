
using System;
namespace SimtekMaui.Models;


using System;

public class Result<T>
{
    public T Value { get; }
    public Exception Error { get; }
    public bool IsSuccess { get; }

    private Result(T value, Exception error, bool isSuccess)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, null, true);
    }

    public static Result<T> Failure(Exception error)
    {
        return new Result<T>(default(T), error, false);
    }

    public void When(Action<T> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(Value);
        }
        else
        {
            onFailure(Error);
        }
    }
}
