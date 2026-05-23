using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Enums;
using SMS.Domain.Util;
using System.Diagnostics;

namespace PeopleAPIServer
{
    public static class ActionResultExtension
    {
        public static ActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Data);

            int statusCode = result.ErrorType.GetStatusCode();
            string traceId = Activity.Current?.TraceId.ToString()
                                ?? controller.HttpContext.TraceIdentifier;

            return controller.StatusCode(statusCode, ApiResponse.Build(statusCode, traceId, result.Errors));
        }

        public static ActionResult ToActionResult(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            int statusCode = result.ErrorType.GetStatusCode();
            string traceId = Activity.Current?.TraceId.ToString()
                                ?? controller.HttpContext.TraceIdentifier;

            return controller.StatusCode(statusCode, ApiResponse.Build(statusCode, traceId, result.Errors));
        }

        static int GetStatusCode(this ErrorType type) => type switch
        {
            ErrorType.Validation => 422,
            ErrorType.NotFound => 404,
            ErrorType.AlreadyExists => 409,
            ErrorType.Reserved => 409,
            ErrorType.InternalError => 500,
            ErrorType.UnsupportedMediaType => 415,
            ErrorType.NotImplemented => 501,
            ErrorType.SizeExceedsLimit => 413,

            _ => 500
        };
    }

    public static class ApiResponse
    {
        public static object Build(int status, string traceId, IEnumerable<Error> errors)
            => new
            {
                status,
                traceId,
                errors = errors.Select(e => new
                {
                    message = e.Message,
                    e.Field,
                    code = e.ErrorCode,
                    e.Metadata
                })
            };
    }
}

