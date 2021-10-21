using Microsoft.Extensions.DependencyInjection;
using SampleApp.Infrastructure.Configuration;
using SampleApp.Infrastructure.Contracts.Configuration;
using SampleApp.Infrastructure.Contracts.Repositories;
using SampleApp.Infrastructure.Data.Models;
using SampleApp.Infrastructure.Repositories;

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
            var cntxt = new SampleContext();
            cntxt.Database.EnsureCreated();
            services.AddSingleton(cntxt);
            services.AddInfrastructureRepositories();
            return services;
        }

        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ISampleAppRepository, SampleAppRepository>();
            services.AddSingleton<ISubSampleAppRepository, SubSampleAppRepository>();
            services.AddSingleton<ISampleRepositoryConfiguration, SampleRepositoryConfiguration>();
            return services;
        }
    }
}
