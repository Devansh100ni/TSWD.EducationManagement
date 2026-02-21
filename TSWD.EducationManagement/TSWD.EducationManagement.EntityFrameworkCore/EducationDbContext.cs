using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.Domain.Extentions;
using TSWD.EducationManagement.Shared.Providers;

namespace TSWD.EducationManagement.EntityFrameworkCore;

public partial class EducationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITenantProvider? tenantProvider;

    public EducationDbContext()
    {
    }

    public EducationDbContext(DbContextOptions<EducationDbContext> options, IHttpContextAccessor httpContextAccessor, ITenantProvider? tenantProvider = null)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        this.tenantProvider = tenantProvider;
    }

    public DbSet<AppTenant> AppTenants => Set<AppTenant>();
    public DbSet<AppUser> AppUser => Set<AppUser>();
    public DbSet<AppRole> AppRole => Set<AppRole>();
    public DbSet<AppPermission> AppPermissions => Set<AppPermission>();
    public DbSet<AppRolePermission> AppRolePermissions => Set<AppRolePermission>();
    public DbSet<AppSchoolGeneralSetting> AppSchoolGeneralSettings => Set<AppSchoolGeneralSetting>();
    public DbSet<AppApiRequestLog> AppApiRequestLogs => Set<AppApiRequestLog>();
    public DbSet<AppClass> AppClasses => Set<AppClass>();
    public DbSet<AppFilter> AppFilters => Set<AppFilter>();
    public DbSet<AppGradePolicy> AppGradePolicies => Set<AppGradePolicy>();
    public DbSet<AppPromotionRule> AppPromotionRules => Set<AppPromotionRule>();
    public DbSet<AppSchoolRule> AppSchoolRules => Set<AppSchoolRule>();
    public DbSet<AppSection> AppSections => Set<AppSection>();
    public DbSet<AppSubject> AppSubjects => Set<AppSubject>();



    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        OnModelCreatingPartial(builder);

        builder.Entity<AppTenant>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Tenants", DbConstants.DefaultSchema);
        });

        builder.Entity<AppUser>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Users", DbConstants.DefaultSchema);
            //b.HasQueryFilter(e =>
            //!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId);
        });

        builder.Entity<AppRole>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Roles", DbConstants.DefaultSchema);
            //b.HasQueryFilter(e =>
            //!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId);
        });

        builder.Entity<AppPermission>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Permissions", DbConstants.DefaultSchema);
        });

        builder.Entity<AppRolePermission>(b =>
        {
            b.ToTable(DbConstants.Prefix + "RolePermissions", DbConstants.DefaultSchema);
            //b.HasNoKey();
        });

        builder.Entity<AppSchoolGeneralSetting>(b =>
        {
            b.ToTable(DbConstants.Prefix + "SchoolGeneralSettings", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppClass>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Classes", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppFilter>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Filter", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppPromotionRule>(b =>
        {
            b.ToTable(DbConstants.Prefix + "PromotionRules", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppGradePolicy>(b =>
        {
            b.ToTable(DbConstants.Prefix + "GradePolicies", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppSchoolRule>(b =>
        {
            b.ToTable(DbConstants.Prefix + "SchoolRules", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppSubject>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Subjects", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppSection>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Sections", DbConstants.DefaultSchema);
            b.HasQueryFilter(e =>
            tenantProvider != null && (!tenantProvider.TenantId.HasValue || e.TenantId == tenantProvider.TenantId));
        });

        builder.Entity<AppApiRequestLog>(b =>
        {
            b.ToTable(DbConstants.Prefix + "ApiRequestLogs", DbConstants.DefaultSchema);
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntityWithMandatoryFields>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreationTime = DateTime.UtcNow;
                entry.Entity.CreatorId = GetCurrentUserId(); // <-- implement this
                entry.Entity.ConcurrencyStamp = Guid.NewGuid().ToString();
                entry.Entity.ExtraProperties = "{}";
                entry.Entity.IsDeleted = false;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModificationTime = DateTime.UtcNow;
                entry.Entity.LastModifierId = GetCurrentUserId();
            }

            if (entry.State == EntityState.Deleted)
            {
                // Convert hard delete to soft delete
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletionTime = DateTime.UtcNow;
                entry.Entity.DeleterId = GetCurrentUserId();
            }
        }

        var entriesTenant = ChangeTracker.Entries<MultiTenant>();

        foreach (var entry in entriesTenant)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = GetCurrentTenantId();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private Guid? GetCurrentUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity.IsAuthenticated)
            return null;

        // Try ASP.NET Identity's claim type
        var userId = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        // Or fallback to JWT "sub" claim
        if (string.IsNullOrEmpty(userId))
            userId = user.FindFirst("sub")?.Value;

        return string.IsNullOrEmpty(userId) ? null : Guid.Parse(userId);
    }

    private Guid? GetCurrentTenantId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity.IsAuthenticated)
            return null;

        // Try ASP.NET Identity's claim type
        var tenantId = user.FindFirst(System.Security.Claims.ClaimTypes.UserData)?.Value;

        // Or fallback to JWT "sub" claim
        if (string.IsNullOrEmpty(tenantId))
            tenantId = user.FindFirst("sub")?.Value;

        return string.IsNullOrEmpty(tenantId) ? null : Guid.Parse(tenantId);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
