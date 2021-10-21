using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp.Infrastructure.Contracts.Repositories
{
    public interface ISubSampleAppRepository
    {
        Task<IEnumerable<SubSample>> GetAllSubSamplesAsync();

        Task<Sample> GetByIdAsync(Guid id);

        Task<IEnumerable<SubSample>> GetSubSamplesAsync(Guid sampleId);

        Task<SubSample> AddSampleAsync(SubSample subSampleForCreation);

        Task<Sample> UpdateSampleAsync(Sample sampleForUpdate);

        Task DeleteSampleAsync(Guid id);
    }
}

