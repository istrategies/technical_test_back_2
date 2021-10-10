using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Mappers;
using SampleApp.Application.Contracts.Validators;
using SampleApp.Infrastructure;

namespace SampleApp.Application.Contracts
{
    public static class Startup
    {
        /// <summary>
        /// Dependency injection for the Application layer
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddContractsLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SampleMapperProfile));

            services.AddTransient<IValidator<SampleFilter>, SampleFilterValidator>();
            services.AddTransient<IValidator<SampleForUpdate>, SampleForUpdateValidator>();
            services.AddTransient<IValidator<SampleForCreate>, SampleForCreateValidator>();
            services.AddTransient<IValidator<SubSample>, SubSampleValidator>();

            return services;
        }
    }
}
