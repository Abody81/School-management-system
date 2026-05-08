namespace SMS.Business.Util.ErrorCodes
{
    public enum BaseErrorCode
    {
        [HttpStatusCode(400)] ValidationError = 1000,
        [HttpStatusCode(404)] NotFound = 1001,
        [HttpStatusCode(409)] AlreadyExists = 1002,
        [HttpStatusCode(500)] InternalError = 1003,
        [HttpStatusCode(415)] UnsupportedMediaType = 1004,
    }
}
