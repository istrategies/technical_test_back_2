using AutoMapper;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Domain.Entities;
using static System.DateTime;

namespace SampleApp.Application.Mapper
{
    public static class ConfigAutoMapper
    {
        public static MapperConfiguration ConfigToReadFromCreate;
        public static MapperConfiguration ConfigToReadFromUpdate;
        public static MapperConfiguration ConfigToCreate;
        public static MapperConfiguration ConfigToRead;
        public static MapperConfiguration ConfigToUpdate;
        
        static ConfigAutoMapper()
        {
            ConfigToReadFromCreate = new MapperConfiguration(cfg => cfg.CreateMap<SampleForCreate, SampleForRead>()
                .ForMember(dest => dest.SampleId, opts => opts.MapFrom(s => s.ToString())));
            ConfigToReadFromUpdate = new MapperConfiguration(cfg => cfg.CreateMap<SampleForUpdate, SampleForRead>()
                .ForMember(dest => dest.SampleId, opts => opts.MapFrom(s => s.ToString())));
            ConfigToRead = new MapperConfiguration(cfg => cfg.CreateMap<Sample, SampleForRead>()
                .ForMember(dest => dest.SampleId, opts => opts.MapFrom(s => s.ToString())));
            ConfigToUpdate = new MapperConfiguration(cfg => cfg.CreateMap<SampleForUpdate, Sample>());
            ConfigToCreate = new MapperConfiguration(cfg => cfg.CreateMap<SampleForCreate, Sample>()
                .ForMember(dest => dest.Created, opts => opts.MapFrom(s => UtcNow)));
        }
    }
}
