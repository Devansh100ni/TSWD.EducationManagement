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
        private readonly IRepository<AppClass> schoolClassRepository;
        private readonly IRepository<AppSection> schoolSectionRepository;
        private readonly IRepository<AppSubject> schoolSubjectRepository;

        public SchoolAcademicSettigs(IRepository<AppGradePolicy> gradePolicyRepository,
                                      IRepository<AppSchoolRule> schoolRuleRepository,
                                      IRepository<AppFilter> filterRepository,
                                      IRepository<AppPromotionRule> promotionRules,
                                      IRepository<AppClass> schoolClassRepository,
                                      IRepository<AppSection> schoolSectionRepository,
                                      IRepository<AppSubject> schoolSubjectRepository)
        {
            this.gradePolicyRepository = gradePolicyRepository;
            this.schoolRuleRepository = schoolRuleRepository;
            this.filterRepository = filterRepository;
            this.promotionRules = promotionRules;
            this.schoolClassRepository = schoolClassRepository;
            this.schoolSectionRepository = schoolSectionRepository;
            this.schoolSubjectRepository = schoolSubjectRepository;
        }

        public async Task CreateUpdateFilters(Guid? tenantId, List<SchoolFilterDto> filters, CancellationToken cancellationToken = default)
        {
            // Only for Master Admin (tenantId == null)
            if (tenantId != null) return;

            var existingFilters = (await filterRepository.GetAllAsync(cancellationToken)).ToList();

            var filtersToRemove = existingFilters.Where(e => !filters.Any(c => c.Id == e.Id)).ToList();
            if (filtersToRemove.Any()) await filterRepository.DeleteRangeAsync(filtersToRemove);

            foreach (var c in filters.Where(x => x.Id != Guid.Empty && existingFilters.Any(e => e.Id == x.Id)))
            {
                var e = existingFilters.First(x => x.Id == c.Id);
                e.FilterKey = c.FilterKey;
                e.FilterValue = c.FilterValue;
                await filterRepository.UpdateAsync(e, cancellationToken);
            }

            var newFilters = filters.Where(x => x.Id == Guid.Empty || !existingFilters.Any(e => e.Id == x.Id)).Select(x => new AppFilter
            {
                Id = Guid.NewGuid(),
                FilterKey = x.FilterKey,
                FilterValue = x.FilterValue,
                TenantId = null
            }).ToList();

            if (newFilters.Any()) await filterRepository.AddRangeAsync(newFilters, cancellationToken);
        }

        public async Task CreateUpdateRules(Guid? tenantId, CreateUpdateAcademicSettingsDto dto, CancellationToken cancellationToken = default)
        {
            // Grade Policies
            var existingGradePolicies = (await gradePolicyRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId);
            if (existingGradePolicies.Any()) await gradePolicyRepository.DeleteRangeAsync(existingGradePolicies);
            var newGradePolicies = dto.GradePolicies.Select(x => new AppGradePolicy { TenantId = tenantId, FromPercent = x.FromPercent, ToPercent = x.ToPercent, Grade = x.Grade }).ToList();
            await gradePolicyRepository.AddRangeAsync(newGradePolicies, cancellationToken);

            // Promotion Rules
            var existingPromotionRules = (await promotionRules.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId);
            if (existingPromotionRules.Any()) await promotionRules.DeleteRangeAsync(existingPromotionRules);
            var newPromotionRules = dto.SchoolPromotionRules.Select(x => new AppPromotionRule { TenantId = tenantId, RuleId = x.RuleId, FilterId = x.FilterId, RuleValue = x.PromotionValue, IsPercent = x.IsPercent }).ToList();
            await promotionRules.AddRangeAsync(newPromotionRules, cancellationToken);

            // School Rules
            var existingRules = (await schoolRuleRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var rulesToRemove = existingRules.Where(e => !dto.SchoolRules.Any(c => c.Id == e.Id)).ToList();
            if (rulesToRemove.Any()) await schoolRuleRepository.DeleteRangeAsync(rulesToRemove);
            foreach (var c in dto.SchoolRules.Where(x => x.Id.HasValue && existingRules.Any(e => e.Id == x.Id)))
            {
                var e = existingRules.First(x => x.Id == c.Id);
                e.RuleName = c.RuleName;
                await schoolRuleRepository.UpdateAsync(e, cancellationToken);
            }
            var newRules = dto.SchoolRules.Where(x => !x.Id.HasValue || !existingRules.Any(e => e.Id == x.Id)).Select(x => new AppSchoolRule { Id = Guid.NewGuid(), RuleName = x.RuleName, TenantId = tenantId }).ToList();
            if (newRules.Any()) await schoolRuleRepository.AddRangeAsync(newRules, cancellationToken);

            // Classes
            var existingClasses = (await schoolClassRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var classesToRemove = existingClasses.Where(e => !dto.SchoolClasses.Any(c => c.Id == e.Id)).ToList();
            if (classesToRemove.Any()) await schoolClassRepository.DeleteRangeAsync(classesToRemove);
            foreach (var c in dto.SchoolClasses.Where(x => x.Id.HasValue && existingClasses.Any(e => e.Id == x.Id)))
            {
                var e = existingClasses.First(x => x.Id == c.Id);
                e.CLassName = c.ClassName;
                await schoolClassRepository.UpdateAsync(e, cancellationToken);
            }
            var newClasses = dto.SchoolClasses.Where(x => !x.Id.HasValue || !existingClasses.Any(e => e.Id == x.Id)).Select(x => new AppClass { Id = Guid.NewGuid(), CLassName = x.ClassName, TenantId = tenantId }).ToList();
            if (newClasses.Any()) await schoolClassRepository.AddRangeAsync(newClasses, cancellationToken);

            // Sections
            var existingSections = (await schoolSectionRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var sectionsToRemove = existingSections.Where(e => !dto.SchoolSections.Any(c => c.Id == e.Id)).ToList();
            if (sectionsToRemove.Any()) await schoolSectionRepository.DeleteRangeAsync(sectionsToRemove);
            foreach (var c in dto.SchoolSections.Where(x => x.Id.HasValue && existingSections.Any(e => e.Id == x.Id)))
            {
                var e = existingSections.First(x => x.Id == c.Id);
                e.AppClassId = c.AppClassId;
                e.SectionName = c.SectionName;
                e.SectionOrder = c.SectionOrder;
                e.Description = c.Description;
                await schoolSectionRepository.UpdateAsync(e, cancellationToken);
            }
            var newSections = dto.SchoolSections.Where(x => !x.Id.HasValue || !existingSections.Any(e => e.Id == x.Id)).Select(x => new AppSection { Id = Guid.NewGuid(), AppClassId = x.AppClassId, SectionName = x.SectionName, SectionOrder = x.SectionOrder, Description = x.Description, TenantId = tenantId }).ToList();
            if (newSections.Any()) await schoolSectionRepository.AddRangeAsync(newSections, cancellationToken);

            // Subjects
            var existingSubjects = (await schoolSubjectRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var subjectsToRemove = existingSubjects.Where(e => !dto.SchoolSubjects.Any(c => c.Id == e.Id)).ToList();
            if (subjectsToRemove.Any()) await schoolSubjectRepository.DeleteRangeAsync(subjectsToRemove);
            foreach (var c in dto.SchoolSubjects.Where(x => x.Id.HasValue && existingSubjects.Any(e => e.Id == x.Id)))
            {
                var e = existingSubjects.First(x => x.Id == c.Id);
                e.SubjectName = c.SubjectName;
                e.ClassId = c.ClassId;
                e.IsOptional = c.IsOptional;
                await schoolSubjectRepository.UpdateAsync(e, cancellationToken);
            }
            var newSubjects = dto.SchoolSubjects.Where(x => !x.Id.HasValue || !existingSubjects.Any(e => e.Id == x.Id)).Select(x => new AppSubject { Id = Guid.NewGuid(), SubjectName = x.SubjectName, ClassId = x.ClassId, IsOptional = x.IsOptional, TenantId = tenantId }).ToList();
            if (newSubjects.Any()) await schoolSubjectRepository.AddRangeAsync(newSubjects, cancellationToken);
        }

        public async Task<AcademicSettingsDto> GetAcademicSettingsAsync(Guid? tenantId, CancellationToken cancellationToken = default)
        {
            var gradePoliciesTask = await gradePolicyRepository.GetAllAsync(x => x.TenantId == tenantId, x => new GradePolicyDto { Id = x.Id, FromPercent = x.FromPercent, ToPercent = x.ToPercent, Grade = x.Grade }, cancellationToken);
            var schoolClassesTask = await schoolClassRepository.GetAllAsync(x => x.TenantId == tenantId, x => new SchoolClassesDto { Id = x.Id, ClassName = x.CLassName }, cancellationToken);
            
            var schoolSectionsTask = await schoolSectionRepository.GetAllAsync(x => x.TenantId == tenantId, x => new SchoolSectionDto { Id = x.Id, AppClassId = x.AppClassId, SectionName = x.SectionName, SectionOrder = x.SectionOrder, Description = x.Description }, cancellationToken);
            
            var schoolSubjectsTask = await schoolSubjectRepository.GetAllAsync();
            var schoolRulesTask = await schoolRuleRepository.GetAllAsync(cancellationToken);
            var filtersTask = await filterRepository.GetAllAsync(cancellationToken);
            var promotionRulesTask = await promotionRules.GetAllAsync(cancellationToken);

            var schoolPromotionRules =
                                from pr in promotionRulesTask
                                join sr in schoolRulesTask
                                    on pr.RuleId equals sr.Id
                                join f in filtersTask
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

            var schoolSubjects = from sub in schoolSubjectsTask
                                 join sc in schoolClassesTask
                                    on sub.ClassId equals sc.Id
                                 where sub.TenantId == tenantId
                                 select new SchoolSubjectDto
                                 {
                                     Id = sub.Id,
                                     SubjectName = sub.SubjectName,
                                     IsOptional = sub.IsOptional,
                                     ClassId = sc.Id,
                                     ClassName = sc.ClassName
                                 };

            var result = new AcademicSettingsDto
            {
                GradePolicies = gradePoliciesTask.ToList(),
                SchoolClasses = schoolClassesTask.ToList(),
                SchoolSections = schoolSectionsTask.ToList(),
                SchoolPromotionRules = schoolPromotionRules.ToList(),
                SchoolSubjects = schoolSubjects.ToList(),
                SchoolRules = schoolRulesTask.Where(x => x.TenantId == tenantId).Select(x => new SchoolRuleDto { Id = x.Id, RuleName = x.RuleName }).ToList(),
                SchoolFilters = filtersTask.Select(x => new SchoolFilterDto { Id = x.Id, FilterKey = x.FilterKey, FilterValue = x.FilterValue }).ToList()
            };

            return result;
        }
    }
}
