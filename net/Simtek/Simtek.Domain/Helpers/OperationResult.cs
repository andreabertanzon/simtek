namespace Simtek.Domain.Helpers;

public class OperationResult<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; }
    public Exception? Error { get; }

    protected OperationResult(bool isSuccess, T data, Exception? error)
    {
        switch (isSuccess)
        {
            case true when error is not null:
                throw new ArgumentException("Operation cannot be successful and have an error");
            case false when error is null:
                throw new ArgumentException("Operation cannot be unsuccessful and have no error");
        }

        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }
    
    public static OperationResult<T> Success(T data) => new(true, data, null);
    public static OperationResult<T> Failure(Exception error) => new(false, default(T), error);
    
    public static implicit operator bool(OperationResult<T> result) => result.IsSuccess;
    
    public static implicit operator T(OperationResult<T> result) => result.Data;
    
    public static implicit operator Exception(OperationResult<T> result) => result.Error ?? new Exception("Operation failed");
    
    public void When(Action<T> onSuccess, Action<Exception> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(Data);
        }
        else
        {
            onFailure(Error!);
        }
    }
    
}