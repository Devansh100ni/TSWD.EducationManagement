using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.SchoolGeneralSettings;
using TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolSettingsController : ControllerBase
    {
        private readonly ISchoolGeneralSettings schoolGeneralSettings;

        public SchoolSettingsController(ISchoolGeneralSettings schoolGeneralSettings)
        {
            this.schoolGeneralSettings = schoolGeneralSettings;
        }

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetSchoolGeneralSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var settings = await schoolGeneralSettings.GetGenralSettingsAsync(tenantId, cancellationToken);
            return Ok(settings);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchoolGeneralSettingsAsync([FromBody] UpdateSchoolGeneralSettingDto input, CancellationToken cancellationToken = default)
        {
            var updatedSettings = await schoolGeneralSettings.UpdateSchoolGeneralSettingsAsync(input, cancellationToken);
            return Ok(updatedSettings);
        }


    }

}
