using System.Net;
using System.Reflection;

namespace SMS.Business.Util.ErrorCodes
{
    public enum ErrorCode
    {
        [HttpStatusCode(200)] None = 0,

        // BaseErrors 1000-1099, and for casting from BaseErrorCodeEnum
        [HttpStatusCode(400)] ValidationError = 1000,
        [HttpStatusCode(404)] NotFound = 1001,
        [HttpStatusCode(409)] AlreadyExists = 1002,
        [HttpStatusCode(500)] InternalError = 1003,
        [HttpStatusCode(415)] UnsupportedMediaType = 1004,
        
        // General 1100-1999
        [HttpStatusCode(400)] InvalidId = 1100,
        [HttpStatusCode(500)] DatabaseFailure = 1101,

        // Person 2000-2999
        [HttpStatusCode(409)] NationalNumberAlreadyExists = 2000,
        [HttpStatusCode(409)] NationalNumberReserved = 2001,

        [HttpStatusCode(409)] PhoneNumberAlreadyExists = 2002,
        [HttpStatusCode(409)] PhoneNumberNumberReserved = 2003,
    }

    public static class AppErrorCodeExtensions
    {
        private static readonly Dictionary<ErrorCode, int> _statusCodes = new();

        static AppErrorCodeExtensions()
        {
            var enumType = typeof(ErrorCode);

            foreach (ErrorCode value in Enum.GetValues(enumType))
            {
                var fieldInfo = enumType.GetField(value.ToString());
                var attribute = fieldInfo?.GetCustomAttribute<HttpStatusCodeAttribute>();

                _statusCodes[value] = attribute != null
                    ? (int)attribute.StatusCode
                    : 500;
            }
        }

        public static int GetStatusCode(this ErrorCode code)
        {
            _statusCodes.TryGetValue(code, out var statusCode);
            return statusCode;
        }
    }
}

[AttributeUsage(AttributeTargets.Field)]
internal class HttpStatusCodeAttribute : Attribute
{
    public HttpStatusCode StatusCode { get; }

    public HttpStatusCodeAttribute(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCodeAttribute(int statusCode)
    {
        StatusCode = (HttpStatusCode)statusCode;
    }
}