using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Helpers;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Application.Service
{
    class SampleAppService : ISampleAppService
    {
        private bool disposedValue;

        public SampleContext SampleContext { get; }
        public IMapper Mapper { get; }

        public SampleAppService(SampleContext sampleContext, IMapper mapper)
        {
            SampleContext = sampleContext;
            Mapper = mapper;
        }

        public async Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation)
        {

            var sample = Mapper.Map<Sample>(sampleForCreation);

            SampleContext.Add(sample);
            await SampleContext.SaveChangesAsync();

            return Mapper.Map<SampleForRead>(sample);
        }

        public async Task DeleteSampleAsync(Guid id)
        {
            var sample = await SampleContext.Sample.FindAsync(id);
            SampleContext.Remove(sample);
            await SampleContext.SaveChangesAsync();
            return;
        }

        public async Task<IEnumerable<SampleForRead>> GetAllSamplesAsync(int rowsNumber, string key)
        {
            var query = from sample in SampleContext.Sample
                        select new Sample
                        {
                            SampleId = sample.SampleId,
                            Name = sample.Name,
                            Created = sample.Created
                        };
            query = rowsNumber > 0 ? query.Take(rowsNumber) : query;
            switch (key) {
                case "SampleId":
                    query = query.OrderBy(x => x.SampleId);
                    break;
                case "Name":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "Created":
                    query = query.OrderBy(x => x.Created);
                    break;
                default:
                    //Nothing
                    break;
            }
            var samples = await query.ToListAsync();
            return Mapper.Map<IEnumerable<SampleForRead>>(samples);
        }

        public async Task<SampleForRead> GetByIdAsync(Guid id)
        {
            var sample = await SampleContext.Sample.Include(s => s.SubSamples).FirstOrDefaultAsync(x => x.SampleId == id);
            if (sample == null)
                return null;
            var mappedSubSamples = Mapper.Map<List<Contracts.DTO.SubSample>>(sample.SubSamples);
            var mappedSample = Mapper.Map<SampleForRead>(sample);

            mappedSample.SubSamples = mappedSubSamples;
            return mappedSample;
        }

        public async Task<IEnumerable<SubSampleForRead>> GetAllSubSamplesAsync(PageParameters pageParameters, DateTimeOffset? greaterThan , DateTimeOffset? lessThan)
        {
            var all = SampleContext.SubSample
                .Include(x => x.SampleRef)
                .AsQueryable();

            var subSamplesFiltered = greaterThan == DateTimeOffset.MinValue ?
                all :
                all.Where(x => x.SampleRef.Created.CompareTo((DateTimeOffset)greaterThan) > 0);
            subSamplesFiltered = lessThan == DateTimeOffset.MinValue ?
                subSamplesFiltered :
                subSamplesFiltered.Where(x => x.SampleRef.Created.CompareTo((DateTimeOffset)lessThan) < 0);
                

            var subSamplesPaged = PagedList<Domain.Entities.SubSample>.ToPagedList(subSamplesFiltered,
                pageParameters.pageNumber,
                pageParameters.pageSize);
            var subSamples = Mapper.Map<List<SubSampleForRead>>(subSamplesPaged);

            return subSamples;
        }

        public async Task<IEnumerable<Contracts.DTO.SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            var subsamples = await SampleContext.SubSample.Where(sb => sb.SampleId == sampleId).ToListAsync();
            return Mapper.Map<List<Contracts.DTO.SubSample>>(subsamples);
        }

        public async Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate)
        {
            var sample = Mapper.Map<Sample>(sampleForUpdate);

            var entity = await SampleContext.Sample.FindAsync(sampleForUpdate.SampleId);
            SampleContext.Entry(entity).State = EntityState.Detached;

            SampleContext.Update(sample);
            await SampleContext.SaveChangesAsync();

            return Mapper.Map<SampleForRead>(sample);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
