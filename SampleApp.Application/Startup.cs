using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Services;
using SampleApp.Infrastructure;

namespace SampleApp.Application
{
    public static class Startup
    {
        /// <summary>
        /// Dependency injection for the Application layer
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddInfrastructureLayer();
            services.AddApplicationLayerServices();
            return services;
        }
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<ISampleAppService, SampleAppService>();
            return services;
        }
    }
}
