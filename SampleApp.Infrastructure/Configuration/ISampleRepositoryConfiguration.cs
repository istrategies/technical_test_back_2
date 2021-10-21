using Microsoft.Extensions.Configuration;
using SampleApp.Infrastructure.Contracts.Configuration;

namespace SampleApp.Infrastructure.Configuration
{
    public class SampleRepositoryConfiguration : ISampleRepositoryConfiguration
    {
        private readonly IConfiguration _configuration;

        public SampleRepositoryConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public int DefaultRowsOfSamples => _configuration.GetValue("DefaultRowsOfSamples", 5);
    }
}