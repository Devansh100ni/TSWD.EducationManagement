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

        public async Task CreateUpdateRules(Guid tenantId, CreateUpdateAcademicSettingsDto createUpdateAcademicSettingsDto, CancellationToken cancellationToken = default)
        {
            var existingGradePolicies = (await gradePolicyRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId);

            if (existingGradePolicies.Any())
            {
                await gradePolicyRepository.DeleteRangeAsync(existingGradePolicies);
            }

            var newGradePolicies = createUpdateAcademicSettingsDto.GradePolicies.Select(x => new AppGradePolicy
            {
                TenantId = tenantId,
                FromPercent = x.FromPercent,
                ToPercent = x.ToPercent,
                Grade = x.Grade
            }).ToList();

            await gradePolicyRepository.AddRangeAsync(newGradePolicies, cancellationToken);

            var existingPromotionRules = (await promotionRules.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId);

            if (existingPromotionRules.Any())
            {
                await promotionRules.DeleteRangeAsync(existingPromotionRules);
            }

            var newPromotionRules = createUpdateAcademicSettingsDto.SchoolPromotionRules.Select(x => new AppPromotionRule
            {
                TenantId = tenantId,
                RuleId = x.RuleId,
                FilterId = x.FilterId,
                RuleValue = x.PromotionValue,
                IsPercent = x.IsPercent
            }).ToList();

            await promotionRules.AddRangeAsync(newPromotionRules, cancellationToken);
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
