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

        [HttpGet("[action]/{tenantId:guid}")]
        public async Task<IActionResult> GetAcademicSettingsAsync(Guid tenantId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                var result = await settings.GetAcademicSettingsAsync(tenantId, cancellationToken);
                return Ok(result);
            }, nameof(GetAcademicSettingsAsync));
        }
    }
}
