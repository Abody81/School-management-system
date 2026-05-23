namespace SMS.Domain.Enums;

public enum ErrorType
{
    None = 0,
    Validation = 1,
    NotFound = 2,
    AlreadyExists = 3,
    Reserved = 4,
    InternalError = 5,
    UnsupportedMediaType = 6,
    SizeExceedsLimit = 7,
    NotImplemented = 8
}

public enum ServiceErrorCodes
{
    ValidationError = 1,
    NotFound = 2,
    AlreadyExists = 3,
    InternalError = 4,
    UnsupportedMediaType = 5,
    NotImplemented = 6,
    ImageSizeExceedsLimit = 7
}


