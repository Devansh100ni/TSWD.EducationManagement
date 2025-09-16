using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Tenents;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService tenantService;

        public TenantsController(ITenantService tenantService)
        {
            this.tenantService = tenantService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetTenantListAsync(PagedRequest request)
        {
            return Ok(await tenantService.ListOfTenants(request.PageNumber, request.PageSize));
        }
        
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> PostAsync([FromForm]CreateUpdateTenantDto dto)
        {
            return Ok(await tenantService.CreateUpdateTenant(dto));
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await tenantService.GetById(id));
        }
    }
}
