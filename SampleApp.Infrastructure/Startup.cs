using Microsoft.Extensions.DependencyInjection;
using SampleApp.Infrastructure.Data.Models;

namespace SampleApp.Infrastructure
{
    public static class Startup
    {
        /// <summary>
        /// Dependency Injection for the Data/Infrastructure layer
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            SampleContext cntxt = new SampleContext();
            cntxt.Database.EnsureCreated();
            services.AddSingleton(cntxt);
            return services;
        }
    }
}
