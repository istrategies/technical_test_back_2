using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleApp.Api.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        private FileLogger logger;
        private readonly IConfiguration _configuration;

        public LoggerProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ILogger CreateLogger(string categoryName)
        {
            logger = new FileLogger(_configuration);
            return logger;
        }

        public void Dispose()
        {
            logger?.Dispose();
        }
    }
}
