using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Contracts.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            CreateMap<SampleForCreate, Sample>();
            CreateMap<Sample, SampleForRead>()
                .ForMember(dest => dest.Sample1Id, act => act.MapFrom(src => src.SampleId));
            CreateMap<Domain.Entities.SubSample, DTO.SubSample>();
            CreateMap<DTO.SubSample, Domain.Entities.SubSample>();
            CreateMap<SampleForUpdate, Sample>();

            /*CreateMap<Domain.Entities.SubSample, SubSampleForRead>()
                .ForMember(dest => dest.SubSampleInfo, act => act.MapFrom(src => src.Info));
            CreateMap<Sample, SubSampleForRead>()
                .ForMember(dest => dest.SampleName, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.SampleCreated, act => act.MapFrom(src => src.Created));*/

            CreateMap<Domain.Entities.SubSample, SubSampleForRead>()
                .ForMember(dest => dest.SubSampleInfo, act => act.MapFrom(src => src.Info))
                .ForMember(dest => dest.SampleId, act => act.MapFrom(src => src.SampleRef.SampleId))
                .ForMember(dest => dest.SampleName, act => act.MapFrom(src => src.SampleRef.Name))
                .ForMember(dest => dest.SampleCreated, act => act.MapFrom(src => src.SampleRef.Created));
        }
    }
}
