using TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement.Application.SchoolAcademicSettings
{
    public class SchoolAcademicSettigs : ISchoolAcademicSettings
    {
        private readonly IRepository<AppGradePolicy> gradePolicyRepository;
        private readonly IRepository<AppSchoolRule> schoolRuleRepository;
        private readonly IRepository<AppFilter> filterRepository;
        private readonly IRepository<AppPromotionRule> promotionRules;

        public SchoolAcademicSettigs(IRepository<AppGradePolicy> gradePolicyRepository,
                                      IRepository<AppSchoolRule> schoolRuleRepository,
                                      IRepository<AppFilter> filterRepository,
                                      IRepository<AppPromotionRule> promotionRules)
        {
            this.gradePolicyRepository = gradePolicyRepository;
            this.schoolRuleRepository = schoolRuleRepository;
            this.filterRepository = filterRepository;
            this.promotionRules = promotionRules;
        }

        public async Task<AcademicSettingsDto> GetAcademicSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var gradePoliciesTask = gradePolicyRepository.GetAllAsync(
                                                                      x => x.TenantId == tenantId,
                                                                      x => new GradePolicyDto
                                                                      {
                                                                          Id = x.Id,
                                                                          FromPercent = x.FromPercent,
                                                                          ToPercent = x.ToPercent,
                                                                          Grade = x.Grade
                                                                      },
                                                                      cancellationToken);

            var schoolRulesTask = schoolRuleRepository.GetAllAsync(cancellationToken);
            var filtersTask = filterRepository.GetAllAsync(cancellationToken);
            var promotionRulesTask = promotionRules.GetAllAsync(cancellationToken);

            await Task.WhenAll(gradePoliciesTask, schoolRulesTask, filtersTask, promotionRulesTask);

            var schoolPromotionRules =
                                from pr in await promotionRulesTask
                                join sr in await schoolRulesTask
                                    on pr.RuleId equals sr.Id
                                join f in await filtersTask
                                    on pr.FilterId equals f.Id
                                where pr.TenantId == tenantId
                                select new SchoolPromotionRuleDto
                                {
                                    PromotionId = pr.Id,
                                    RuleId = sr.Id,
                                    FilterId = f.Id,

                                    RuleName = sr.RuleName,
                                    FilterKey = f.FilterKey,
                                    FilterValue = f.FilterValue,

                                    PromotionValue = pr.RuleValue,
                                    IsPercent = pr.IsPercent
                                };


            var result = new AcademicSettingsDto
            {
                GradePolicies = (await gradePoliciesTask).ToList(),
                SchoolPromotionRules = schoolPromotionRules.ToList()
            };

            return result;
        }
    }
}
