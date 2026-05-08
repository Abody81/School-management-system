using SMS.Business.Util.ErrorCodes;
using System.Diagnostics;

namespace SMS.Business.Util
{
    [DebuggerStepThrough]
    public class Result
    {
        public record ErrorDetail(string Message, ErrorCode Code, string Field);

        public bool IsSuccess { get; protected set; } = true;
        public List<ErrorDetail> Errors { get; protected set; } = [];
        public ErrorCode BaseErrorCode { get; protected set; }

        public Result(BaseErrorCode baseCode)
        {
            BaseErrorCode = (ErrorCode)baseCode;
        }

        protected Result() { }

        public static Result Success() => new() { IsSuccess = true, BaseErrorCode = ErrorCode.None };

        public static Result Failure(string error, ErrorCode code, string field = "global" ) => new Result { IsSuccess = false, Errors = [new(error, code, field)], BaseErrorCode = code };

        public void AddError(string message, ErrorCode code, string field = "global")
        {
            IsSuccess = false;
            Errors.Add(new(message, code, field));
        }
    }

    [DebuggerStepThrough]
    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T data)
           => new() { IsSuccess = true, BaseErrorCode = ErrorCode.None, Data = data };

        public new static Result<T> Failure(string error, ErrorCode code, string field = "global") => new()
        {
            IsSuccess = false,
            BaseErrorCode = code,
            Errors = [new(error, code, field)]
        };

        public static Result<T> Failure(Result failedResult) => new()
        {
            IsSuccess = false,
            BaseErrorCode = failedResult.BaseErrorCode,
            Errors = failedResult.Errors
        };
    }
}