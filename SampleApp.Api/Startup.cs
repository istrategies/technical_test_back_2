using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleApp.Api.Filters;
using SampleApp.Api.Logger;
using SampleApp.Application;
using SampleApp.Application.Contracts;
using SampleApp.Application.Contracts.Services;
using System.IO;

namespace SampleApp.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            // Swagger config
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "SampleApp v1",
                    Version = "1.0"
                });
            });

            // En caso de no ser una prueba extraeria esto a un método de extensión dónde se incluirian los diferentes servicios y otros elementos en el inyector de dependencias
            services.AddTransient(typeof(ISampleAppService), typeof(SampleAppService));
            services.AddFluentValidation();

            services.AddApplicationLayer();
            services.AddContractsLayer();
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger)
        {
            loggerFactory.AddProvider(new LoggerProvider(_configuration));

            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                var taskLog = context.WriteLog(logger);
                var taskAction = next();

                await taskLog;
                await taskAction;
            });

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
