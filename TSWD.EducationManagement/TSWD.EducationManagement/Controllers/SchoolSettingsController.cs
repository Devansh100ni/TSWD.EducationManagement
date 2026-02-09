using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using TSWD.EducationManagement.Application.SchoolGeneralSettings;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings;
using TSWD.EducationManagement.Shared.Caching;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolSettingsController(ISchoolGeneralSettings schoolGeneralSettings,
                                          IAppCacheService cache) : CommonControllerBase
    {

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetSchoolGeneralSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            return await ExecuteAsync(async ct =>
            {
                var cacheKey = CacheKeys.SchoolGeneralSettings(tenantId);

                try
                {
                    return await cache.GetOrSetAsync(
                        cacheKey,
                        () => schoolGeneralSettings.GetGenralSettingsAsync(tenantId, cancellationToken),
                        cancellationToken
                    );
                }
                catch (Exception ex) when (
                    ex is RedisTimeoutException ||
                    ex is RedisConnectionException ||
                    ex is RedisServerException)
                {
                    // Redis fallback → DB
                    return await schoolGeneralSettings
                        .GetGenralSettingsAsync(tenantId, cancellationToken);
                }

            }, nameof(GetSchoolGeneralSettingsAsync));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchoolGeneralSettingsAsync([FromBody] UpdateSchoolGeneralSettingDto input, CancellationToken cancellationToken = default)
        {
            return await ExecuteAsync(async ct =>
            {
                var cacheKey = CacheKeys.SchoolGeneralSettings();
                await cache.RemoveAsync(cacheKey, cancellationToken);

                try
                {
                    return await cache.GetOrSetAsync(
                                cacheKey,
                                () => schoolGeneralSettings.UpdateSchoolGeneralSettingsAsync(input, cancellationToken),
                                cancellationToken
                                );
                }
                catch (Exception ex) when (
                    ex is RedisTimeoutException ||
                    ex is RedisConnectionException ||
                    ex is RedisServerException)
                {
                    // Redis fallback → DB
                    return await schoolGeneralSettings.UpdateSchoolGeneralSettingsAsync(input, cancellationToken);
                }

            }, nameof(UpdateSchoolGeneralSettingsAsync));
        }


    }
}
