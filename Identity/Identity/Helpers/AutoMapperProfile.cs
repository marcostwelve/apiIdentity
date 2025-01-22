using AutoMapper;
using WebAPI.Dominio;
using WebAPI.Dominio.Dto;
using WebAPI.Dominio.NovaPasta;

namespace Identity.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
