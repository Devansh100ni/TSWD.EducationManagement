using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TSWD.EducationManagement.Shared.Caching
{
    public class RedisCacheService : IAppCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> logger;

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
        {
            _cache = cache;
            this.logger = logger;
        }

        public async Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> factory,
            CancellationToken cancellationToken = default)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError($"Error Occered while Get or Set Radis Exception: \n Message: {ex.Message} \n Stack trace: {ex.StackTrace} \n ==================================================");
                throw;
            }

        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}
