using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Users
{
    public interface IUserService
    {
        Task<PagedResult<UsersDtos>> GetAllUsersAsync(PagedRequest input, Guid? tenantId = null);
    }
}
