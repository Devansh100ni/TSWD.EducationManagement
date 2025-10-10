using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace TSWD.EducationManagement.Permissions
{
    public class PermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;

        public PermissionAttribute(string permission)
        {
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            var checker = context.HttpContext.RequestServices.GetRequiredService<IPermissionChecker>();
            var hasPermission = await checker.HasPermissionAsync(Guid.Parse(userId), _permission);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
