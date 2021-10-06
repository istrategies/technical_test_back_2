using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleApp.Api
{
    /// <summary>
    /// Program class, start class of a program in net core
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main and start method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Init application and configuration with substitutions of appsettings.json variables
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
