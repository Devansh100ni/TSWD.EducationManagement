using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore;

namespace TSWD.EducationManagement.Shared.Logging
{
    public static class LogApiCalls
    {
        public static async Task LogAsync(
            HttpContext context,
            Stopwatch stopwatch,
            Exception? exception = null,
            int statusCode = 200,
            string? optionalBody = null
            )
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            // Resolve DbContext via DI
            var dbContext = context.RequestServices.GetService(typeof(EducationDbContext)) as EducationDbContext;
            if (dbContext == null)
                throw new InvalidOperationException("EducationDbContext not registered in DI.");

            var request = context.Request;
            var response = context.Response;


            string body = "[NOT LOGGED]";
            string headers = string.Join(", ", request.Headers.Select(h => $"{h.Key}:{h.Value}"));

            bool isMultipart = request.ContentType?.Contains("multipart/form-data") == true;

            // Read body for text content only
            if (!isMultipart && request.ContentLength > 0 && IsTextBasedContentType(request.ContentType))
            {
                request.EnableBuffering();

                using var reader = new StreamReader(
                    request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true);

                body = Sanitize(await reader.ReadToEndAsync());
                request.Body.Position = 0;

                if (string.IsNullOrEmpty(body) && !string.IsNullOrEmpty(optionalBody))
                    body = Sanitize(optionalBody);
            }

            // File uploads → log metadata only
            if (isMultipart)
            {
                var files = request.Form.Files;
                body = $"[FILE UPLOAD] Files={files.Count}, Names=[{string.Join(", ", files.Select(f => f.FileName))}]";
            }

            var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tenantIdClaim = context.User?.FindFirst(ClaimTypes.UserData)?.Value;

            Guid? userId = null;
            Guid? tenantId = null;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid parsedGuid))
            {
                userId = parsedGuid;
            }
            
            if (tenantIdClaim != null && Guid.TryParse(tenantIdClaim, out Guid parsedTenantIdGuid))
            {
                tenantId = parsedTenantIdGuid;
            }


            bool succeeded = statusCode < 400;

            var log = new AppApiRequestLog
            {
                Path = request.Path,
                Method = request.Method,
                QueryString = request.QueryString.ToString(),
                Headers = headers,
                Body = body,
                StatusCode = statusCode,
                Succeeded = succeeded,
                DurationMs = stopwatch.ElapsedMilliseconds,
                StackTrace = exception?.StackTrace,
                ErrorMessage = exception?.Message,
                InnerException = exception?.InnerException?.ToString(),
                UserId = userId
            };

            if (exception != null)
            {
                log.ErrorMessage = Sanitize(exception.Message);
                log.InnerException = Sanitize(exception.InnerException?.Message);
                log.StackTrace = Sanitize(exception.StackTrace);
            }
            else if (!succeeded)
            {
                log.ErrorMessage = $"Request failed with status code {statusCode}";
            }

            dbContext.AppApiRequestLogs.Add(log);
            await dbContext.SaveChangesAsync();
        }

        private static bool IsTextBasedContentType(string? contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                return false;

            return contentType.StartsWith("application/json") ||
                   contentType.StartsWith("text/") ||
                   contentType.Contains("application/xml");
        }

        private static string Sanitize(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = input.Replace("\0", string.Empty);
            return input.Length > 8000 ? input[..8000] : input;
        }
    }
}
