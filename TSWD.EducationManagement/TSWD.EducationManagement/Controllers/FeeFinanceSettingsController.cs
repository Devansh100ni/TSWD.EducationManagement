using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.FeeFinance;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.FeeFinance;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeFinanceSettingsController : CommonControllerBase
    {
        private readonly IFeeFinanceSettings settings;

        public FeeFinanceSettingsController(IFeeFinanceSettings settings)
        {
            this.settings = settings;
        }

        [HttpGet("[action]/{tenantId:guid?}")]
        public async Task<IActionResult> GetFeeFinanceSettingsAsync([FromRoute] Guid? tenantId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                var result = await settings.GetFeeFinanceSettingsAsync(tenantId, cancellationToken);
                return Ok(result);
            }, nameof(GetFeeFinanceSettingsAsync));
        }

        [HttpPost("[action]/{tenantId:guid?}")]
        public async Task<IActionResult> CreateUpdateFeeFinanceSettings([FromRoute] Guid? tenantId, [FromBody] FeeFinanceSettingsDto dto, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async ct =>
            {
                await settings.CreateUpdateFeeFinanceSettingsAsync(tenantId, dto, cancellationToken);
                return Ok(true);
            }, nameof(CreateUpdateFeeFinanceSettings));
        }
    }
}
