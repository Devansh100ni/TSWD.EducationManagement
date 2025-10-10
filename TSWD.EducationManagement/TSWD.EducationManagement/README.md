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



1. Create a Permissions Table

This will define individual permissions like:
Add Student
Add Teacher
Add Class
Add Book
etc.

2. Create a RolePermissions Table
This table links Roles to Permissions. You need to make it Tenant-specific, so each tenant can have different role-permission mappings.
Note: This enables multi-tenant role customization, which means "Admin" for Tenant A can have different permissions from "Admin" for Tenant B.

1. Sample Use Case

You want to assign to the Admin role (for Tenant X) the following permissions:
Add Student
Add Teacher
Add Book
Add Class

Workflow:
Insert those permissions into AppPermissions.
Link them to the AppRoles entry for "Admin" + Tenant X in AppRolePermissions.

Benefits of This Approach
Fully dynamic: Add new permissions without schema change.
Multi-tenant aware: Different tenants can customize roles independently.
Fine-grained access control.

AppTenants (Id)
  ↑         ↑
  |         |
AppUsers   AppRoles (Id)
                ↑
         AppRolePermissions
                ↑
         AppPermissions
