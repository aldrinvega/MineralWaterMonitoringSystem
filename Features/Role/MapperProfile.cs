using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Role;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Roles, GetRoleAsync.DTO.Roles>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users
                .Select(u => new GetRoleAsync.DTO.Users
                {
                    Fullname = u.FullName,
                    UserId = u.Id
                })
            ));
    }
}