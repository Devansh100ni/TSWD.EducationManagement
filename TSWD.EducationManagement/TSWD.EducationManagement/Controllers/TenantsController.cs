using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Tenents;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : CommonControllerBase
    {
        private readonly ITenantService tenantService;

        public TenantsController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetTenantListAsync(PagedRequest request)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await tenantService.ListOfTenants(request.PageNumber, request.PageSize);
                return response;
            });
        }

        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> PostAsync([FromForm] CreateUpdateTenantDto dto)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await tenantService.CreateUpdateTenant(dto);
                return response;
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return await ExecuteAsync(async () =>
            {
                var response = await tenantService.GetById(id);
                return response;
            });
        }
    }
}
