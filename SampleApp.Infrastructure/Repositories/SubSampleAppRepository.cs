using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Contracts.Repositories;

namespace SampleApp.Infrastructure.Repositories
{
    public class SubSampleAppRepository : ISubSampleAppRepository
    {
        protected DbContext DbContext;
        protected DbSet<SubSample> DbSet;

        public Task<IEnumerable<SubSample>> GetAllSubSamplesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Sample> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            throw new NotImplementedException();
        }

        public async Task<SubSample> AddSampleAsync(SubSample subSampleForCreation)
        {
            DbSet.Add(subSampleForCreation);
            await SaveChangesAsync();
            return subSampleForCreation;
        }

        public Task<Sample> UpdateSampleAsync(Sample sampleForUpdate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSampleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}

