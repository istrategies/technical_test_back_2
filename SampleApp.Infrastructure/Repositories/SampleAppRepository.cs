using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Contracts.Repositories;
using SampleApp.Infrastructure.Data.Models;

namespace SampleApp.Infrastructure.Repositories
{
    public class SampleAppRepository : ISampleAppRepository
    {
        protected SampleContext DbContext;
        protected DbSet<Sample> DbSet;

        public SampleAppRepository(SampleContext dbContext)
        {
            DbContext = dbContext;
            SetDbSet();
        }

        public async Task<IEnumerable<Sample>> GetAllSamplesAsync()
        {
            IQueryable<Sample> query = DbSet;
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<Sample> GetByIdAsync(Guid id)
        {
            IQueryable<Sample> query = DbSet;
            var result = await query.ToListAsync();
            return result.FirstOrDefault(s => s.SampleId == id);
        }

        public async Task AddSampleAsync(Sample sampleForCreation)
        {
            DbSet.Add(sampleForCreation);
            await SaveChangesAsync();
        }

        public async Task UpdateSampleAsync(Sample sampleForUpdate)
        {
            DbSet.Attach(sampleForUpdate);
            await using (DbContext)
                DbContext.Entry(sampleForUpdate).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteSampleAsync(Sample sampleToDelete)
        {
            await using (DbContext)
                if (DbContext.Entry(sampleToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(sampleToDelete);
                }

            DbSet.Remove(sampleToDelete);
            await SaveChangesAsync();
        }
        public void Dispose()
        {
        }

        protected void SetDbSet()
        {
            DbSet = DbContext.Set<Sample>();
        }

        private async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}

