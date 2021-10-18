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
        private IMapper _iMapper;

        public SampleAppService(ISampleAppRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<IEnumerable<SampleForRead>> GetAllSamplesAsync()
        {
            IEnumerable<Sample> samplesRepository;
            _iMapper = ConfigAutoMapper.ConfigToRead.CreateMapper();
            using (_sampleRepository)
                samplesRepository = await _sampleRepository.GetAllSamplesAsync();
            return _iMapper.Map<IEnumerable<SampleForRead>>(samplesRepository);
        }

        public async Task<SampleForRead> GetByIdAsync(Guid id)
        {
            Sample sampleRepository;
            _iMapper = ConfigAutoMapper.ConfigToRead.CreateMapper();
            using (_sampleRepository)
                sampleRepository = await _sampleRepository.GetByIdAsync(id);
            return _iMapper.Map<SampleForRead>(sampleRepository);
        }

        public async Task<IEnumerable<Contracts.DTO.SubSample>> GetSubSamplesAsync(Guid sampleId)
        {
            Sample sampleRepository;
            _iMapper = ConfigAutoMapper.ConfigToRead.CreateMapper();
            using (_sampleRepository)
                sampleRepository = await _sampleRepository.GetByIdAsync(sampleId);
            var sampleToRead = _iMapper.Map<SampleForRead>(sampleRepository);
            return sampleToRead.SubSamples;
        }

        public async Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation)
        {
            _iMapper = ConfigAutoMapper.ConfigToCreate.CreateMapper();
            var sampleToCreate = _iMapper.Map<Sample>(sampleForCreation);
            using (_sampleRepository)
                await _sampleRepository.AddSampleAsync(sampleToCreate);
            _iMapper = ConfigAutoMapper.ConfigToReadFromCreate.CreateMapper();
            return _iMapper.Map<SampleForRead>(sampleForCreation);
        }

        public async Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate)
        {
            _iMapper = ConfigAutoMapper.ConfigToUpdate.CreateMapper();
            var sampleToUpdate = _iMapper.Map<Sample>(sampleForUpdate);
            using (_sampleRepository)
                await _sampleRepository.UpdateSampleAsync(sampleToUpdate);
            _iMapper = ConfigAutoMapper.ConfigToReadFromCreate.CreateMapper();
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

