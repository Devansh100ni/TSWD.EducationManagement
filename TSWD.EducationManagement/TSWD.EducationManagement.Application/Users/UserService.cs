using Microsoft.EntityFrameworkCore;
using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Common;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<AppUser> repository;
        private readonly IRepository<AppRole> roleRepository;

        public UserService(IRepository<AppUser> repository, IRepository<AppRole> roleRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
        }

        public async Task<Result<UsersDtos>> CreateUpdateUserAsync(CreateUpdateUsersDto input)
        {
            if (input == null)
                return Result<UsersDtos>.Fail("Invalid user data.");

            try
            {
                AppUser? entity;

                if (input.Id.HasValue)
                {
                    entity = await repository.GetByIdAsync(input.Id.Value);

                    if (entity == null)
                        return Result<UsersDtos>.Fail("User not found, cannot update.");

                    entity.UserName = input.UserName;
                    entity.NormalizedUserName = input.NormalizedUserName;
                    entity.Email = input.Email;
                    entity.NormalizedEmail = input.NormalizedEmail;
                    entity.Name = input.Name;
                    entity.Surname = input.Surname;
                    entity.RoleId = input.RoleId;
                    entity.IsActive = input.IsActive;
                    entity.PhoneNumber = input.PhoneNumber;
                    entity.PhoneNumberConfirmed = input.PhoneNumberConfirmed;
                    entity.EmailConfirmed = input.EmailConfirmed;
                    entity.TwoFactorEnabled = input.TwoFactorEnabled;
                    entity.LockoutEnd = input.LockoutEnd;
                    entity.LockoutEnabled = input.LockoutEnabled;
                    entity.AccessFailedCount = input.AccessFailedCount;
                    entity.ShouldChangePasswordOnNextLogin = input.ShouldChangePasswordOnNextLogin;
                    entity.LastPasswordChangeTime = input.LastPasswordChangeTime;
                    entity.EntityVersion = (entity.EntityVersion ?? 0) + 1; // increment version

                    await repository.UpdateAsync(entity);
                }
                else
                {
                    // Create new user
                    entity = new AppUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = input.UserName,
                        NormalizedUserName = input.NormalizedUserName,
                        Email = input.Email,
                        NormalizedEmail = input.NormalizedEmail,
                        Name = input.Name,
                        Surname = input.Surname,
                        RoleId = input.RoleId,
                        TenantId = input.TenantId,
                        PasswordHash = Encrypt.HashPassword(input.PasswordHash),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        IsActive = input.IsActive,
                        PhoneNumber = input.PhoneNumber,
                        PhoneNumberConfirmed = false,
                        EmailConfirmed = false,
                        TwoFactorEnabled = input.TwoFactorEnabled,
                        LockoutEnabled = input.LockoutEnabled,
                        AccessFailedCount = 0,
                        ShouldChangePasswordOnNextLogin = false,
                        EntityVersion = 1
                    };

                    await repository.AddAsync(entity);
                }

                // Prepare DTO
                var dto = new UsersDtos
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    FullName = $"{entity.Name} {entity.Surname}".Trim(),
                    Email = entity.Email,
                    RoleId = entity.RoleId,
                    IsActive = entity.IsActive
                };

                return Result<UsersDtos>.Ok(dto);
            }
            catch (Exception ex)
            {
                return Result<UsersDtos>.Fail($"Error while creating/updating user: {ex.Message}");
            }
        }

        public async Task<PagedResult<UsersDtos>> GetAllUsersAsync(PagedRequest input, Guid? tenantId = null)
        {
            var query = await repository.AsQueryable();
            var rolesQuery = await roleRepository.AsQueryable();

            int skip = (input.PageNumber - 1) * input.PageSize;

            if (tenantId.HasValue)
                query = query.Where(x => x.TenantId == tenantId.Value);

            var totalCount = await query.CountAsync();

            var users = await (from u in query
                               join r in rolesQuery on u.RoleId equals r.Id into roleGroup
                               from role in roleGroup.DefaultIfEmpty() // left join
                               orderby u.Name
                               select new UsersDtos
                               {
                                   Id = u.Id,
                                   FullName = u.Name + " " + u.Surname,
                                   Email = u.Email,
                                   IsActive = u.IsActive,
                                   RoleName = role != null ? role.Name : string.Empty, // <-- role name instead of roleid
                                   UserName = u.UserName,
                                   RoleId = u.RoleId

                               })
                      .Skip(skip)
                      .Take(input.PageSize)
                      .ToListAsync();

            if (!users.Any() && input.PageNumber > 1)
            {
                input.PageNumber--;

                skip = (input.PageNumber - 1) * input.PageSize;

                await GetAllUsersAsync(input, tenantId);
            }

            return new PagedResult<UsersDtos>(users.ToList(), totalCount);
        }

        public async Task<Result<CreateUpdateUsersDto>> GetUserByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<CreateUpdateUsersDto>.Fail("Invalid user ID.");

            var user = await repository.GetByIdAsync(id);

            if (user == null)
                return Result<CreateUpdateUsersDto>.Fail("No User Found");

            var dto = new CreateUpdateUsersDto
            {
                Id = user.Id,
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                Name = user.Name,
                Surname = user.Surname,
                RoleId = user.RoleId,
                TenantId = user.TenantId,
                IsActive = user.IsActive,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                EmailConfirmed = user.EmailConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd,
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount,
                ShouldChangePasswordOnNextLogin = user.ShouldChangePasswordOnNextLogin,
                LastPasswordChangeTime = user.LastPasswordChangeTime,
                EntityVersion = user.EntityVersion
            };

            return Result<CreateUpdateUsersDto>.Ok(dto);
        }
    }
}
