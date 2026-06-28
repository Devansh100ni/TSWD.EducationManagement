using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.SchoolAcademicSettings;
using TSWD.EducationManagement.Controllers.Base;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolAcademicSettingsController : CommonControllerBase
    {
        private readonly ISchoolAcademicSettings settings;

        public SchoolAcademicSettingsController(ISchoolAcademicSettings settings)
        {
            this.settings = settings;
        }

        [HttpGet("[action]/{tenantId:guid?}")]
        public async Task<IActionResult> GetAcademicSettingsAsync([FromRoute] Guid? tenantId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                var result = await settings.GetAcademicSettingsAsync(tenantId, cancellationToken);
                return Ok(result);
            }, nameof(GetAcademicSettingsAsync));
        }

        [HttpPost("[action]/{tenantId:guid?}")]
        public async Task<IActionResult> CreateUpdateRules([FromRoute] Guid? tenantId, [FromBody] Domain.DTOs.SchoolAcademicSettings.CreateUpdateAcademicSettingsDto dto, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                await settings.CreateUpdateRules(tenantId, dto, cancellationToken);
                return Ok(true);
            }, nameof(CreateUpdateRules));
        }

        [HttpPost("[action]/{tenantId:guid?}")]
        public async Task<IActionResult> CreateUpdateFilters([FromRoute] Guid? tenantId, [FromBody] List<Domain.DTOs.SchoolAcademicSettings.SchoolFilterDto> filters, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                await settings.CreateUpdateFilters(tenantId, filters, cancellationToken);
                return Ok(true);
            }, nameof(CreateUpdateFilters));
        }
    }
}
