using AccountService.Models;
using AccountService.Models.DTOs;
using AutoMapper;

namespace AccountService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationType, ApplicationTypeDto>();
                cfg.CreateMap<ApplicationTypeDto, ApplicationType>().ForMember(dest => dest.CreateDate, opt => opt.Ignore());
            });

            return mappingConfig;
        }
    }
}
