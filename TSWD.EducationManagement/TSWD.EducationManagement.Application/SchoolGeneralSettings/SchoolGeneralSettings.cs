using TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement.Application.SchoolGeneralSettings
{
    public class SchoolGeneralSettings : ISchoolGeneralSettings
    {
        private readonly IRepository<AppSchoolGeneralSetting> repository;
        private readonly IRepository<AppTenant> tenantRepository;

        public SchoolGeneralSettings(IRepository<AppSchoolGeneralSetting> repository,
                                     IRepository<AppTenant> tenantRepository)
        {
            this.repository = repository;
            this.tenantRepository = tenantRepository;
        }

        public async Task<SchoolGeneralSettingDto> GetGenralSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = (await repository.AsQueryable()).Where(x => x.TenantId == tenantId).FirstOrDefault();

                if (result == null)
                {
                    var tenant = await tenantRepository.FindAsync(tenantId);

                    if (tenant == null)
                    {
                        throw new Exception("Tenant not found.");
                    }

                    var newSetting = new AppSchoolGeneralSetting
                    {
                        Id = Guid.NewGuid(),
                        TenantId = tenantId,
                        SchoolName = tenant.Name,
                        SchoolCode = tenant.Name,
                    };

                    await repository.AddAsync(newSetting);
                }

                result = (await repository.AsQueryable()).Where(x => x.TenantId == tenantId).FirstOrDefault();

                if (result == null) throw new NullReferenceException("No School Settings Found");

                var dto = new SchoolGeneralSettingDto
                {
                    Id = result.Id,
                    TenantId = result.TenantId,
                    SchoolName = result.SchoolName,
                    SchoolCode = result.SchoolCode,
                    Address = result.Address,
                    Email = result.Email,
                    Phone = result.Phone,
                    LogoUrl = result.LogoUrl,
                    TimeZone = result.TimeZone,
                    DateFormat = result.DateFormat
                };

                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SchoolGeneralSettingDto> UpdateSchoolGeneralSettingsAsync(UpdateSchoolGeneralSettingDto input, CancellationToken cancellationToken = default)
        {
            try
            {
                var setting = (await repository.AsQueryable()).Where(x => x.Id == input.Id).FirstOrDefault();

                if (setting == null) throw new NullReferenceException("No School Settings Found to Update");

                setting.SchoolName = input.SchoolName;
                setting.SchoolCode = input.SchoolCode;
                setting.Address = input.Address;
                setting.Email = input.Email;
                setting.Phone = input.Phone;
                setting.LogoUrl = input.LogoUrl;

                await repository.UpdateAsync(setting);

                var dto = new SchoolGeneralSettingDto
                {
                    Id = setting.Id,
                    TenantId = setting.TenantId,
                    SchoolName = setting.SchoolName,
                    SchoolCode = setting.SchoolCode,
                    Address = setting.Address,
                    Email = setting.Email,
                    Phone = setting.Phone,
                    LogoUrl = setting.LogoUrl,
                    TimeZone = setting.TimeZone,
                    DateFormat = setting.DateFormat
                };
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
