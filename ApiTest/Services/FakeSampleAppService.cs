using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApp.Application.Contracts.Mapper;

namespace ApiTest.Services
{
    class FakeSampleAppService : ISampleAppService
    {
        private bool disposedValue;
        private readonly List<Sample> Samples;
        private readonly List<SampleApp.Domain.Entities.SubSample> SubSamples;
        //Seed data path
        public const string SAMPLE_SEED_PATH = @"Data\SampleSeed\Samples.json";
        public const string SUBSAMPLE_SEED_PATH = @"Data\SampleSeed\SubSamples.json";

        public IMapper Mapper { get; }

        public FakeSampleAppService() {
            var root = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            Samples = JsonSerializer.Deserialize<List<Sample>>(File.ReadAllText(Path.Combine(root, SAMPLE_SEED_PATH)));
            SubSamples = JsonSerializer.Deserialize<List<SampleApp.Domain.Entities.SubSample>>(File.ReadAllText(Path.Combine(root, SUBSAMPLE_SEED_PATH)));
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });
            Mapper = mapperConfig.CreateMapper();
        }

        public Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSampleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SampleForRead>> GetAllSamplesAsync()
        {
            var query = from sample in Samples
                        select new Sample
                        {
                            SampleId = sample.SampleId,
                            Name = sample.Name,
                            Created = sample.Created
                        };
            var samples = query.ToList();
            return (Task<IEnumerable<SampleForRead>>)Mapper.Map<IEnumerable<SampleForRead>>(samples);
        }

        public Task<IEnumerable<SubSampleForRead>> GetAllSubSamplesAsync(PageParameters pageParameters, DateTimeOffset? greaterThan, DateTimeOffset? lessThan)
        {
            throw new NotImplementedException();
        }

        public Task<SampleForRead> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SampleApp.Application.Contracts.DTO.SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            throw new NotImplementedException();
        }

        public Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate)
        {
            throw new NotImplementedException();
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
