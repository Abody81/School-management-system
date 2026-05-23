//using SMS.Domain.Enums;
//using System.Diagnostics;

//#pragma warning disable

//namespace SMS.Domain.Util;

//[DebuggerStepThrough]
//public class ServiceResult
//{
//    public bool IsSuccess { get; protected set; }

//    public ServiceErrorCodes ErrorCode { get; protected set; }

//    public string? Message { get; protected set; }

//    public static ServiceResult Success() => new() { IsSuccess = true };

//    public static ServiceResult Failure(string message, ServiceErrorCodes code) => 
//        new() { IsSuccess = false, ErrorCode = code, Message = message};

//}

//public class ServiceResult<T> : ServiceResult
//{
//    public T? Data;

//    public static ServiceResult<T> Success(T data) => new() { IsSuccess = true, Data = data };

//    public static ServiceResult<T> Failure(string message, ServiceErrorCodes code) => 
//        new() { IsSuccess = false, ErrorCode = code, Message = message};


//    public static implicit operator ServiceResult<T>(T value)
//    => Success(value);

//    public static implicit operator ServiceResult<T>(ServiceError error)
//    => Failure(error.Message, error.ServiceErrorCode, error.Metadata);
//}
