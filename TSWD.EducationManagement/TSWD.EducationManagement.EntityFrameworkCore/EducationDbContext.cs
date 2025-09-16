using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.EntityFrameworkCore;

public partial class EducationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public EducationDbContext()
    {
    }

    public EducationDbContext(DbContextOptions<EducationDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<AppTenant> AppTenants { get; set; }
    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<AppRole> AppRole { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;");

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
        });

        builder.Entity<AppRole>(b =>
        {
            b.ToTable(DbConstants.Prefix + "Roles", DbConstants.DefaultSchema);
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

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
