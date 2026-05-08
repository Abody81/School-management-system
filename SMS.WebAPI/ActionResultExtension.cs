using Microsoft.AspNetCore.Mvc;
using SMS.Business.Util;
using SMS.Business.Util.ErrorCodes;
using System.Diagnostics;
using static SMS.Business.Util.Result;

namespace PeopleAPIServer
{
    public static class ActionResultExtension
    {
        public static ActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Data);

            int statusCode = result.BaseErrorCode.GetStatusCode();
            string traceId = Activity.Current?.TraceId.ToString()
                                ?? controller.HttpContext.TraceIdentifier;

            return controller.StatusCode(statusCode, ApiResponse.Build(statusCode, traceId, result.Errors));
        }

        public static ActionResult ToActionResult(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            int statusCode = result.BaseErrorCode.GetStatusCode();
            string traceId = Activity.Current?.TraceId.ToString()
                                ?? controller.HttpContext.TraceIdentifier;

            return controller.StatusCode(statusCode, ApiResponse.Build(statusCode, traceId, result.Errors));
        }
    }

    public static class ApiResponse
    {
        public static object Build(int status, string traceId, IEnumerable<ErrorDetail> errors)
            => new
            {
                status,
                traceId,
                errors = errors.Select(e => new
                {
                    message = e.Message,
                    code = (int)e.Code,
                    e.Field
                })
            };
    }
}

