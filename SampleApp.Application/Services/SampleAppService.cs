using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Mapper;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Contracts.Repositories;

namespace SampleApp.Application.Services
{
    public class SampleAppService : ISampleAppService
    {
        private readonly ISampleAppRepository _sampleRepository;
        private readonly IMapper _iMapper;

        public SampleAppService(ISampleAppRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
            _iMapper = ConfigAutoMapper.Config.CreateMapper();
        }

        public async Task<IEnumerable<SampleForRead>> GetAllSamplesAsync()
        {
            IEnumerable<Sample> samplesRepository;
            using (_sampleRepository)
                samplesRepository = await _sampleRepository.GetAllSamplesAsync();
            return _iMapper.Map<IEnumerable<SampleForRead>>(samplesRepository);
        }

        public async Task<SampleForRead> GetByIdAsync(Guid id)
        {
            Sample sampleRepository;
            using (_sampleRepository)
                sampleRepository = await _sampleRepository.GetByIdAsync(id);
            return _iMapper.Map<SampleForRead>(sampleRepository);
        }

        public async Task<IEnumerable<Contracts.DTO.SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            Sample sampleRepository;
            using (_sampleRepository)
                sampleRepository = await _sampleRepository.GetByIdAsync(sampleId);
            var sampleToRead = _iMapper.Map<SampleForRead>(sampleRepository);
            return sampleToRead.SubSamples;
        }

        public async Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation)
        {
            var sampleToCreate = _iMapper.Map<Sample>(sampleForCreation);
            using (_sampleRepository)
                await _sampleRepository.AddSampleAsync(sampleToCreate);
            var sampleCreated = _iMapper.Map<SampleForRead>(sampleForCreation);
            sampleCreated.Created = sampleToCreate.Created;
            return sampleCreated;
        }

        public async Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate)
        {
            var sampleToUpdate = _iMapper.Map<Sample>(sampleForUpdate);
            using (_sampleRepository)
                await _sampleRepository.UpdateSampleAsync(sampleToUpdate);
            return _iMapper.Map<SampleForRead>(sampleForUpdate);
        }

        public async Task DeleteSampleAsync(Guid id)
        {
            using (_sampleRepository)
            {
                var sampleToDelete = await _sampleRepository.GetByIdAsync(id);
                await _sampleRepository.DeleteSampleAsync(sampleToDelete);
            }
        }

        public void Dispose()
        {
        }
    }
}

