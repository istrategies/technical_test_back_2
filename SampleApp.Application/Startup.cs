using Microsoft.Extensions.DependencyInjection;
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
            return services;
        }
    }
}
