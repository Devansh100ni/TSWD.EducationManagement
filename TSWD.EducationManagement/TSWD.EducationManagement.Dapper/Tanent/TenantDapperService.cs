using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Dapper.Infrastructure;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Dapper.Tanent
{
    public class TenantDapperService : ITenantDapperService
    {
        private readonly IDapperRepository _dapperRepo;
        private readonly string _tenantQueryPath;

        public TenantDapperService(IDapperRepository dapperRepo)
        {
            _dapperRepo = dapperRepo;

            //var assembly = typeof(TenantDapperService).Assembly;
            //foreach (var resourceName in assembly.GetManifestResourceNames())
            //{
            //    Console.WriteLine(resourceName);
            //}
        }

        public async Task<PagedResult<TenantDto>> GetTenantsAsync(int pageNumber, int pageSize)
        {
            string sql = await SqlResourceHelper.GetSqlAsync<TenantDapperService>("GetTenantsWithUserCount.sql");

            int skip = (pageNumber - 1) * pageSize;

            using var multi = await _dapperRepo.QueryMultipleAsync(sql, new { Skip = skip, Take = pageSize });

            var tenants = (await multi.ReadAsync<TenantDto>()).ToList();
            var totalCount = await multi.ReadSingleAsync<int>();

            if (!tenants.Any() && pageNumber > 1)
            {
                pageNumber--;

                skip = (pageNumber - 1) * pageSize;

                await GetTenantsAsync(pageNumber, pageSize);
            }

            return new PagedResult<TenantDto>(tenants, totalCount);
        }
    }
}
