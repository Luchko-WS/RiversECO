using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RiversECO.Cache.Contracts;

namespace RiversECO.Cache.Configuration
{
    public static class CacheSetupExt
    {
        public static IServiceCollection RegisterCache(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMemoryCache, MemoryCache>()
                .AddSingleton<IAppMemoryCache, AppMemoryCache>();
        }
    }
}
