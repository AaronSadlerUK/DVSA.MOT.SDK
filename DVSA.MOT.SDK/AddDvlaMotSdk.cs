using DVSA.MOT.SDK.Interfaces;
using DVSA.MOT.SDK.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DVSA.MOT.SDK
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDvlaMotSdk(this IServiceCollection services)
        {
            services.AddHttpClient<IProcessApiResponse, ProcessApiResponse>();
            services.AddScoped<ISingleVehicleService, SingleVehicleService>();
            services.AddScoped<IAllVehiclesService, AllVehiclesService>();
            return services;
        }
    }
}