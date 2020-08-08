using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RiversECO.DataContext.Configuration
{
    public static class DataContextSetupExt
    {
        public static IServiceCollection ConfigureDataContextForSqlServer(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<DataContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionString);
                });
        }
    }
}
