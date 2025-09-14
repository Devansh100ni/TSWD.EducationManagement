using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TSWD.EducationManagement.Domain.Entities;

namespace TSWD.EducationManagement.EntityFrameworkCore;

public partial class EducationDbContext : DbContext
{
    public EducationDbContext()
    {
    }

    public EducationDbContext(DbContextOptions<EducationDbContext> options)
        : base(options)
    {
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

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
