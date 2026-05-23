//using SMS.Domain.Enums;

//#pragma warning disable

//namespace SMS.Domain.Util;

//public record OldError(string Message, ServiceErrorCodes Code, string Field, Dictionary<string, object> Metadata = default);

//public class OldResult
//{
//    public bool IsSuccess { get; protected set; } = true;
//    public List<OldError> Errors { get; protected set; } = [];
//    public ServiceErrorCodes BaseErrorCode { get; protected set; }

//    public OldResult(ErrorType baseCode)
//    {
//        BaseErrorCode = (ServiceErrorCodes)baseCode;
//    }

//    protected OldResult() { }

//    public static OldResult Success() => new() { IsSuccess = true, BaseErrorCode = ServiceErrorCodes.None };

//    public static OldResult Failure(string error, ServiceErrorCodes code, string field = default, Dictionary<string, object> metadata = default) =>
//        new OldResult { IsSuccess = false, Errors = [new(error, code, field, metadata)], BaseErrorCode = code };

//    public static OldResult Failure(OldResult failedResult)
//       => new() { IsSuccess = false, BaseErrorCode = failedResult.BaseErrorCode, Errors = failedResult.Errors };

//    public void AddError(string message, ServiceErrorCodes code, string? field = default, Dictionary<string, object>? metadata = default)
//    {
//        IsSuccess = false;
//        Errors.Add(new(message, code, field, metadata));
//    }
//}


//public class OldResult<T> : OldResult
//{
//    public T? Data { get; set; }

//    public static OldResult<T> Success(T data)
//       => new() { IsSuccess = true, BaseErrorCode = ServiceErrorCodes.None, Data = data };

//    public new static OldResult<T> Failure(string error, ServiceErrorCodes code, string? field = default, Dictionary<string, object>? Metadata = default)
//        => new() { IsSuccess = false, BaseErrorCode = code, Errors = [new(error, code, field)] };

//    public static OldResult<T> Failure(OldResult failedResult)
//        => new() { IsSuccess = false, BaseErrorCode = failedResult.BaseErrorCode, Errors = failedResult.Errors };

//    public static implicit operator OldResult<T>(T value)
//   => Success(value);
//}