using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Group;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Groups, GetGroupsAsync.DTOs.Group>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Payers.Select(u => new GetGroupsAsync.DTOs.User
            {
                FullName = u.Fullname,
                Id = u.Id 
            })));

    }
}