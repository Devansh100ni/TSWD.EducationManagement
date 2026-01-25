using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using TSWD.EducationManagement.Domain.Base;
using TSWD.EducationManagement.Shared.Common;
using TSWD.EducationManagement.Shared.Helpers;
using TSWD.EducationManagement.Shared.Logging;

namespace TSWD.EducationManagement.Controllers.Base
{
    public class CommonControllerBase : ControllerBase
    {
        protected async Task<IActionResult> ExecuteAsync<TResponse>(
           Func<Task<TResponse>> action,
           string? methodName = null,
           string? optionalBody = null)
        {
            var stopwatch = Stopwatch.StartNew();
            Exception? exception = null;
            int statusCode = 200;

            try
            {
                var response = await action();
                stopwatch.Stop();

                // If response implements IResponse, populate TimeTaken
                if (response is IResponse baseResponse)
                    baseResponse.TimeTaken = stopwatch.Elapsed;

                if (response is IActionResult actionResult)
                    return actionResult;

                if (response != null && IsResultType(response))
                {
                    dynamic result = response;

                    if (result.Success)
                        return Ok(result);

                    statusCode = StatusCodes.Status400BadRequest;
                    return BadRequest(new
                    {
                        message = result.Message
                    });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                exception = ex;
                stopwatch.Stop();
                (statusCode, IActionResult result) = HandleException(ex, methodName);
                return result;
            }
            finally
            {
                await LogApiCalls.LogAsync(HttpContext, stopwatch, exception, statusCode, optionalBody);
            }
        }

        private (int statusCode, IActionResult result) HandleException(Exception ex, string methodName)
        {
            if (ex.InnerException != null)
                ex = ex.InnerException;

            HttpStatusCode statusCode;
            string message;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = Constants.DataErrors.AccessDenied;
                    break;

                case ArgumentException argEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = argEx.Message;
                    break;
                case InvalidOperationException iOEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = iOEx.Message;
                    break;
                case KeyNotFoundException kNFE:
                    statusCode = HttpStatusCode.NotFound;
                    message = kNFE.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = Constants.DataErrors.InternalServerError;
                    break;
            }

            var result = StatusCode((int)statusCode, new { error = message });
            return ((int)statusCode, result);
        }

        private static bool IsResultType(object response)
        {
            var type = response.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>);
        }
    }
}
