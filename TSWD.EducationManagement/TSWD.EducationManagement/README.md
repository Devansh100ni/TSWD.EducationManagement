## Dependency Diagram

                ┌──────────────────────────────┐
                │   TSWD.EducationManagement   │   (API / Presentation)
                └───────────────┬──────────────┘
                                │
                                ▼
                ┌──────────────────────────────┐
                │ TSWD.EducationManagement.App │   (Application Layer)
                └───────┬─────────┬────────────┘
                        │         │
       ┌────────────────┘         └────────────────┐
       ▼                                           ▼
┌──────────────────────────────┐       ┌──────────────────────────────┐
│ TSWD.EducationManagement.EF  │       │ TSWD.EducationManagement.Dapper │  (Infrastructure)
└───────────────┬──────────────┘       └──────────────┬───────────────┘
                │                                     │
                ▼                                     ▼
      ┌──────────────────────────────┐       ┌──────────────────────────────┐
      │   TSWD.EducationManagement   │       │   TSWD.EducationManagement   │
      │           .Domain            │       │           .Domain            │
      └──────────────────────────────┘       └──────────────────────────────┘
                                │
                                ▼
                ┌──────────────────────────────┐
                │   TSWD.EducationManagement   │   (Shared Utilities)
                │            .Shared           │
                └──────────────────────────────┘


## Scaffold Command

Scaffold-DbContext "Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context EducationDbContext -Project TSWD.EducationManagement.EntityFrameworkCore -StartupProject TSWD.EducationManagement


Scaffold-DbContext "Server=DESKTOP-6D1OCD8;Database=EducationDb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir TempEntities -Context EducationDbContext -Project TSWD.EducationManagement.EntityFrameworkCore -StartupProject TSWD.EducationManagement