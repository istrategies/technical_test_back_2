using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Contracts.Mapper;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Service;
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
            
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<ISampleAppService, SampleAppService>();
            return services;
        }

    }
}
