using SMS.Domain.Enums;

#pragma warning disable

namespace SMS.Domain.Util;

public class Result
{
    public bool IsSuccess { get; protected set; } = true;
    public List<Error> Errors { get; protected set; } = [];
    public ErrorType ErrorType { get; protected set; }

    protected Result() { }    
    public Result(ErrorType errorType) { ErrorType = errorType; }

    public static Result Success() => new() { IsSuccess = true };

    public static Result Failure(ErrorType errorType, string errorCode, string message,
        string field = default, Dictionary<string, object>? metadata = default) =>
        new()
        {
            IsSuccess = false,
            Errors = [new(errorType, errorCode, message, field, metadata)],
            ErrorType = errorType
        };

    public static Result Failure(Error error) =>
       new()
       {
           IsSuccess = false,
           Errors = [new(error.ErrorType, error.ErrorCode, error.Message, error.Field, error.Metadata)],
           ErrorType = error.ErrorType
       };

    public static Result Failure(Result failedResult) =>
      new()
      {
          IsSuccess = false,
          Errors = failedResult.Errors,
          ErrorType = failedResult.ErrorType
      };

    public void AddError(Error error)
    {
        IsSuccess = false;
        Errors.Add(new(error.ErrorType, error.ErrorCode, error.Message, error.Field, error.Metadata));
    }


    public static implicit operator Result(Error value)
    => Failure(value);
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public static Result<T> Success(T data)
       => new() { IsSuccess = true, Data = data };

    public new static Result<T> Failure(ErrorType errorType, string errorCode, string message,
        string? field = default, Dictionary<string, object>? metadata = default)
        => new()
        {
            IsSuccess = false,
            Errors = [new(errorType, errorCode, message, field, metadata)],
            ErrorType = errorType
        };

      public static Result<T> Failure(Result<T> failedResult) =>
      new()
      {
          IsSuccess = false,
          Errors = failedResult.Errors,
          ErrorType = failedResult.ErrorType
      };

    public static Result<T> Failure(Result failedResult) =>
      new()
      {
          IsSuccess = false,
          Errors = failedResult.Errors,
          ErrorType = failedResult.ErrorType
      };

    public static Result<T> Failure(Error error) =>
       new()
       {
           IsSuccess = false,
           Errors = [new(error.ErrorType, error.ErrorCode, error.Message, error.Field, error.Metadata)],
           ErrorType = error.ErrorType
       };

    public static Result<T> Failure(Error error, Dictionary<string, object>? additionalMetadata = default)
    {
        var mergedMetadata = error.Metadata is not null
            ? new Dictionary<string, object>(error.Metadata)
            : new Dictionary<string, object>();

        if (additionalMetadata is not null)
        {
            foreach (var kvp in additionalMetadata)
            { 
                mergedMetadata.TryAdd(kvp.Key, kvp.Value);
            }
        }

        return new Result<T>
        {
            IsSuccess = false,
            Errors = [new(error.ErrorType, error.ErrorCode, error.Message, error.Field, mergedMetadata)],
            ErrorType = error.ErrorType
        };
    }

    public static implicit operator Result<T>(T value)
    => Success(value);

    public static implicit operator Result<T>(Error value)
    => Failure(value);
}