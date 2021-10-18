using System;
using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Domain.Entities;
using SubSample = SampleApp.Application.Contracts.DTO.SubSample;

namespace SampleApp.Application.Mapper
{
    public static class ConfigAutoMapper
    {
        public static MapperConfiguration Config;

        static ConfigAutoMapper()
        {

            Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubSample, Domain.Entities.SubSample>();
                cfg.CreateMap<Domain.Entities.SubSample, SubSample>()
                    .ForMember(s => s.SubSampleId, op => op.Ignore());
                cfg.CreateMap<SampleForCreate, Sample>()
                    .ForMember(dest => dest.Created, opts => opts.MapFrom(s => DateTime.UtcNow))
                    .ForMember(s => s.SubSamples, opts => opts.MapFrom(o => o.SubSamples));
                cfg.CreateMap<SampleForUpdate, Sample>()
                    .ForMember(s => s.SubSamples, opts => opts.MapFrom(s => s.SubSamples));
                cfg.CreateMap<Sample, SampleForRead>()
                    .ForMember(s => s.SampleId, op => op.Ignore())
                    .ForMember(s => s.SubSamples, opts => opts.MapFrom(s => s.SubSamples));
                cfg.CreateMap<SampleForCreate, SampleForRead>()
                    .ForMember(s => s.SampleId, op => op.Ignore())
                    .ForMember(s => s.SubSamples, opts => opts.MapFrom(s => s.SubSamples));
                cfg.CreateMap<SampleForUpdate, SampleForRead>()
                    .ForMember(s => s.SampleId, op => op.Ignore())
                    .ForMember(s => s.SubSamples, opts => opts.MapFrom(s => s.SubSamples));
            });
        }
    }
}
