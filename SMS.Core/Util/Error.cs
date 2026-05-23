using SMS.Domain.Enums;

namespace SMS.Domain.Util;

public record struct Error
{
    public Error(ErrorType errorType, string errorCode, string message,
        string? field = default, Dictionary<string, object>? metadata = default)
    {
        ErrorType = errorType;
        Message = message;
        ErrorCode = errorCode;
        Field = field;
        Metadata = metadata;
    }

    public ErrorType ErrorType { get; set; }
    public string Message { get; set; } = null!;
    public string ErrorCode { get; set; } = null!;
    public string? Field { get; set; }
    public Dictionary<string, object>? Metadata { get; set; } = default;
}

