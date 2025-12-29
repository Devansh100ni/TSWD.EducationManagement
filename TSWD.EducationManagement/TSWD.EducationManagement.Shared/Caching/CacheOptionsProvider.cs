using Microsoft.Extensions.Caching.Distributed;

namespace TSWD.EducationManagement.Shared.Caching
{
    public class CacheOptionsProvider
    {
        public static DistributedCacheEntryOptions Default =>
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(2)
        };

        public static DistributedCacheEntryOptions Long =>
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };
    }
}
