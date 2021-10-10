using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using Entities = SampleApp.Domain.Entities;
using DTO = SampleApp.Application.Contracts.DTO;

namespace SampleApp.Application.Contracts.Mappers
{
    public class SampleMapperProfile : Profile
    {
        public SampleMapperProfile()
        {
            CreateMap<SampleForCreate, Entities.Sample>();
            CreateMap<SampleForUpdate, Entities.Sample>();
            CreateMap<Entities.Sample, SampleForRead>().ForMember(x => x.Code, y => y.MapFrom(z => z.SampleId.IdEncode()));
            CreateMap<Entities.SubSample, FilterSampleForRead>()
                .ForMember(x => x.SampleCreated, y => y.MapFrom(y => y.SampleRef.Created))
                .ForMember(x => x.SampleName, y => y.MapFrom(y => y.SampleRef.Name))
                .ForMember(x => x.SampleCode, y => y.MapFrom(z => z.SampleId.IdEncode()))
                .ForMember(x => x.SubSampleCode, y => y.MapFrom(y => y.SubSampleId.IdEncode()))
                .ForMember(x => x.SubSampleInfo, y => y.MapFrom(y => y.Info));
            CreateMap<DTO.SubSample, Entities.SubSample>();
            CreateMap<Entities.SubSample, DTO.SubSample>();
            CreateMap<Entities.SubSample, DTO.SubSampleForRead>().ForMember(x => x.Code, y => y.MapFrom(z => z.SampleId.IdEncode()));
        }
    }
}
