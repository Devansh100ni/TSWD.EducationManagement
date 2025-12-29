using System;
using System.Collections.Generic;
using System.Text;

namespace TSWD.EducationManagement.Shared.Caching
{
    public interface IAppCacheService
    {
        Task<T> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        CancellationToken cancellationToken = default);

        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}
