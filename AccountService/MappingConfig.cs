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

                cfg.CreateMap<Account, AccountDto>();
                cfg.CreateMap<AccountDto, Account>().ForMember(dest => dest.CreateDate, opt => opt.Ignore());
                cfg.CreateMap<Account, AccountDto>().ForMember(dest => dest.AppTypeName, opt => opt.MapFrom(s => s.ApplicationType.Name));

            });

            return mappingConfig;
        }
    }
}
