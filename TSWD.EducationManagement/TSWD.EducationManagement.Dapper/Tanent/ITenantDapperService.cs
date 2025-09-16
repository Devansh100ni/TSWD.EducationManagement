using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Dapper.Tanent
{
    public interface ITenantDapperService
    {
        Task<PagedResult<TenantDto>> GetTenantsAsync(int pageNumber, int pageSize);
    }
}
