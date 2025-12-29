using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace TSWD.EducationManagement.Shared.Caching
{
    public class RedisCacheService : IAppCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> factory,
            CancellationToken cancellationToken = default)
        {
            var cachedData = await _cache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<T>(cachedData)!;
            }

            var data = await factory();

            await _cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(data),
                CacheOptionsProvider.Default,
                cancellationToken
            );

            return data;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}
