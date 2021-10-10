using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Application.Contracts.Services
{
    public class SampleAppService : ISampleAppService, IDisposable
    {
        private bool _disposed;
        private readonly SampleContext _dbContext;
        private readonly IMapper _mapper;
        private readonly int _maxRows;
        public SampleAppService(SampleContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _maxRows = configuration.GetValue<int>("maxRows");
        }

        public async Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation)
        {
            if (await _dbContext.Sample.AnyAsync(x => x.SampleId == sampleForCreation.SampleId)) return null;
            var subSampleIds = sampleForCreation.SubSamples.Select(x => x.SubSampleId);
            if (await _dbContext.SubSample.AnyAsync(x => subSampleIds.Contains(x.SubSampleId))) return null;

            var entity = _mapper.Map<Sample>(sampleForCreation);
            _dbContext.Sample.Add(entity);
            await _dbContext.SaveChangesAsync();

            return  _mapper.Map<SampleForRead>(entity);
        }

        public async Task DeleteSampleAsync(Guid id)
        {
            var item = await _dbContext.Sample.Include(x => x.SubSamples).SingleOrDefaultAsync(x => x.SampleId == id);

            foreach(var subItems in item.SubSamples)
            {
                _dbContext.SubSample.Remove(subItems);
            }

            _dbContext.Sample.Remove(item);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SampleForRead>> GetAllSamplesAsync()
        {
            var items = await _dbContext.Sample.Include(x => x.SubSamples).AsNoTracking().Take(_maxRows).ToArrayAsync();
            return _mapper.Map<IEnumerable<SampleForRead>>(items);
        }

        public async Task<IEnumerable<FilterSampleForRead>> GetAllSamplesAsync(SampleFilter filter)
        {
            var items = await _dbContext.SubSample.AsNoTracking()
                             .Include(x => x.SampleRef)
                             .OrderByDescending(x => x.SampleRef.Created)
                             .Where(x => x.SampleRef.Created >= filter.StartCreated && x.SampleRef.Created <= filter.EndCreated)
                             .Skip(filter.Rows * filter.CurrentPage - 1)
                             .Take(filter.Rows)
                             .ToListAsync();

            return _mapper.Map<IEnumerable<FilterSampleForRead>>(items);
        }

        public async Task<SampleForRead> GetByIdAsync(Guid id)
        {
            var item = await _dbContext.Sample.AsNoTracking().Include(x => x.SubSamples).SingleOrDefaultAsync(x => x.SampleId == id);
            return _mapper.Map<SampleForRead>(item);
        }

        public async Task<IEnumerable<DTO.SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            var items = await _dbContext.SubSample.AsNoTracking().Where(x => x.SampleId == sampleId).ToListAsync();
            return _mapper.Map<IEnumerable<DTO.SubSample>>(items);
        }

        public async Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate)
        {
            var item = await _dbContext.Sample.Include(x => x.SubSamples).SingleOrDefaultAsync(x => x.SampleId == sampleForUpdate.SampleId);
            if (item == null) return null;

            item.Name = sampleForUpdate.Name;

            var childsToUpdateIds = sampleForUpdate.SubSamples.Select(x => x.SubSampleId);
            var childsOriginalIds = item.SubSamples.Select(x => x.SubSampleId);

            var childsToRemove = item.SubSamples.Where(x => !childsToUpdateIds.Contains(x.SubSampleId));
            var childsToAdd = sampleForUpdate.SubSamples.Where(x => !childsOriginalIds.Contains(x.SubSampleId));

            foreach(var child in item.SubSamples.Where(x => childsToUpdateIds.Contains(x.SubSampleId))) 
            {
                var updateChild = sampleForUpdate.SubSamples.First(x => x.SubSampleId == child.SubSampleId);
                child.Info = updateChild.Info;
            }

            foreach(var child in childsToRemove)
            {
                _dbContext.SubSample.Remove(child);
            }

            foreach(var child in childsToAdd)
            {
                var entityChild = _mapper.Map<Domain.Entities.SubSample>(child);
                entityChild.SampleId = item.SampleId;
                _dbContext.SubSample.Add(entityChild);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SampleForRead>(item);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            _disposed = true;
        }

    }
}


