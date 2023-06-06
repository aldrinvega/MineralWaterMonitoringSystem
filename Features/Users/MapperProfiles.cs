using AutoMapper;

namespace MineralWaterMonitoring.Features.Users;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Domain.Users, GetUsersAsync.UsersAsyncQueryResult>()
            .ForMember(dest => dest.GroupCode, opt => opt.MapFrom(src => src.Group.GroupCode))
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.GroupName));
    }
}