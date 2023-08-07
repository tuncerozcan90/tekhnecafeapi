using AutoMapper;
using TekhneCafe.Core.DTOs.AppRole;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRole, AppRoleListDto>();
            CreateMap<AppRoleAddDto, AppRole>();
            CreateMap<AppRoleUpdateDto, AppRole>();
        }
    }
}
