using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application;
using SampleApp.Application.Contracts.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using SampleApp.Application.Contracts.DTO;
using Microsoft.Extensions.Logging;
using System.IO;
using SampleApp.Application.Helpers;

namespace SampleApp.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation();
            // Validators
            services.AddTransient<IValidator<SampleForCreate>, SampleForCreateValidator>();
            services.AddTransient<IValidator<SubSample>, SubSampleValidator>();
            services.AddTransient<IValidator<PageParameters>, PageParemetersValidator>();

            //DI Logger
            var currentPath = Directory.GetCurrentDirectory();
            services.AddSingleton<IRequestLogger>(new RequestLogger($"{currentPath}"+Configuration.GetValue<string>("LogFile")));

            // Swagger config
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "SampleApp v1",
                    Version = "1.0"
                });
            });

            services.AddApplicationLayer();
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger config
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleApp v1");
            });
        }
    }
}
