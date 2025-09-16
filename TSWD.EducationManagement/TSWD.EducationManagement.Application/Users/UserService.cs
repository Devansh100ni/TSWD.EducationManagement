using Microsoft.EntityFrameworkCore;
using TSWD.EducationManagement.Dapper;
using TSWD.EducationManagement.Dapper.Tanent;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<AppUser> repository;

        public UserService(IRepository<AppUser> repository)
        {
            this.repository = repository;
        }

        public async Task<PagedResult<UsersDtos>> GetAllUsersAsync(PagedRequest input, Guid? tenantId = null)
        {
            var query = await repository.AsQueryable();
            int skip = (input.PageNumber - 1) * input.PageSize;

            if (tenantId.HasValue)
                query = query.Where(x => x.TenantId == tenantId.Value);

            var totalCount = await query.CountAsync();

            var users = query
                            .OrderBy(x => x.Name)
                            .Skip(skip)
                            .Take(input.PageSize)
                            .Select(x => new UsersDtos
                            {
                                Id = x.Id,
                                FullName = x.Name + " " + x.Surname,
                                Email = x.Email,
                                IsActive = x.IsActive,
                                RoleId = x.RoleId,
                                UserName = x.UserName
                            }).AsEnumerable();

            if (!users.Any() && input.PageNumber > 1)
            {
                input.PageNumber--;

                skip = (input.PageNumber - 1) * input.PageSize;

                await GetAllUsersAsync(input, tenantId);
            }

            return new PagedResult<UsersDtos>(users.ToList(), totalCount);
        }
    }
}
