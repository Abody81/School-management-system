using SMS.Domain.Enums;
using System.Diagnostics;

#pragma warning disable

namespace SMS.Domain.Util;

public interface IServiceResult 
{
    bool IsSuccess { get; }
    ServiceError? Error { get; }
}

[DebuggerStepThrough]
public readonly struct ServiceResult : IServiceResult
{
    public bool IsSuccess { get; init; }
    public ServiceError? Error { get; init; }

    public static ServiceResult Success() => new() { IsSuccess = true };
    public static ServiceResult Failure(ServiceError error) => new() { IsSuccess = false, Error = error };

    public static implicit operator ServiceResult(ServiceError error) => Failure(error);
}

[DebuggerStepThrough]
public readonly struct ServiceResult<T> : IServiceResult
{
    public bool IsSuccess { get; init; }
    public ServiceError? Error { get; init; }
    public T? Data { get; init; }

    public static ServiceResult<T> Success(T data) => new() { IsSuccess = true, Data = data };
    public static ServiceResult<T> Failure(ServiceError error) => new() { IsSuccess = false, Error = error };

    public static implicit operator ServiceResult<T>(T value) => Success(value);
    public static implicit operator ServiceResult<T>(ServiceError error) => Failure(error);

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ServiceError, TResult> onFailure)
        => IsSuccess ? onSuccess(Data!) : onFailure(Error!.Value);
}

public readonly record struct ServiceError(string message);
