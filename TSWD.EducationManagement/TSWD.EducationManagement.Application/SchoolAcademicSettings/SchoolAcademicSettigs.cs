using TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement.Application.SchoolAcademicSettings
{
    public class SchoolAcademicSettigs : ISchoolAcademicSettings
    {
        private readonly IRepository<AppGradePolicy> gradePolicyRepository;
        private readonly IRepository<AppSchoolRule> schoolRuleRepository;

        public SchoolAcademicSettigs(IRepository<AppGradePolicy> gradePolicyRepository,
                                      IRepository<AppSchoolRule> schoolRuleRepository)
        {
            this.gradePolicyRepository = gradePolicyRepository;
            this.schoolRuleRepository = schoolRuleRepository;
        }

        public async Task<AcademicSettingsDto> GetAcademicSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var gradePolicies = await gradePolicyRepository.GetAllAsync(x => x.TenantId == tenantId,
                                                                        x => new GradePolicyDto
                                                                        {
                                                                            Id = x.Id,
                                                                            FromPercent = x.FromPercent,
                                                                            ToPercent = x.ToPercent,
                                                                            Grade = x.Grade
                                                                        },
                                                                        cancellationToken);

            var schoolRules = await schoolRuleRepository.GetAllAsync(x => x.TenantId == tenantId);




            var result = new AcademicSettingsDto
            {
                GradePolicies = gradePolicies.ToList()
            };


            throw new NotImplementedException();
        }
    }
}
