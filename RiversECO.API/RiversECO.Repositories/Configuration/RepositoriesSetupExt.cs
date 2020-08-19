using Microsoft.Extensions.DependencyInjection;
using RiversECO.Contracts.Repositories;

namespace RiversECO.Repositories.Configuration
{
    public static class RepositoriesSetupExt
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICriteriasRepository, CriteriasRepository>()
                .AddScoped<IRiversRepository, RiversRepository>()
                .AddScoped<ILakesRepository, LakesRepository>()
                .AddScoped<IWaterObjectsRepository, WaterObjectsRepository>();
        }
    }
}
