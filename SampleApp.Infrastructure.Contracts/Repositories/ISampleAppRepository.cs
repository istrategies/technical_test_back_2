using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp.Infrastructure.Contracts.Repositories
{
    public interface ISampleAppRepository : IDisposable
    {
        Task<IEnumerable<Sample>> GetAllSamplesAsync();

        Task<Sample> GetByIdAsync(Guid id);
        
        Task AddSampleAsync(Sample sampleForCreation);

        Task UpdateSampleAsync(Sample sampleForUpdate);

        Task DeleteSampleAsync(Sample sampleToDelete);
    }
}

