using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.SchoolGeneralSettings;
using TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings;
using TSWD.EducationManagement.Shared.Caching;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolSettingsController : ControllerBase
    {
        private readonly ISchoolGeneralSettings schoolGeneralSettings;
        private readonly IAppCacheService cache;

        public SchoolSettingsController(ISchoolGeneralSettings schoolGeneralSettings, IAppCacheService cache)
        {
            this.schoolGeneralSettings = schoolGeneralSettings;
            this.cache = cache;
        }

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetSchoolGeneralSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var cacheKey = CacheKeys.SchoolGeneralSettings();

            var settings = await cache.GetOrSetAsync(
                            cacheKey,
                            () => schoolGeneralSettings.GetGenralSettingsAsync(tenantId, cancellationToken),
                            cancellationToken
                            );

            return Ok(settings);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchoolGeneralSettingsAsync([FromBody] UpdateSchoolGeneralSettingDto input, CancellationToken cancellationToken = default)
        {
            var cacheKey = CacheKeys.SchoolGeneralSettings();
            await cache.RemoveAsync(cacheKey, cancellationToken);

            var updatedSettings = await cache.GetOrSetAsync(
                            cacheKey,
                            () => schoolGeneralSettings.UpdateSchoolGeneralSettingsAsync(input, cancellationToken),
                            cancellationToken
                            );
            return Ok(updatedSettings);
        }
    }
}
