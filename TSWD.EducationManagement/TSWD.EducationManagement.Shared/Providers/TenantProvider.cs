using Microsoft.AspNetCore.Http;

namespace TSWD.EducationManagement.Shared.Providers
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? TenantId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id");
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    return null;
                return Guid.Parse(claim.Value);
            }
        }

        public bool IsApplicationAdministrator =>
            _httpContextAccessor.HttpContext?.User?.IsInRole("ApplicationAdministrator") ?? false;
    }
}
