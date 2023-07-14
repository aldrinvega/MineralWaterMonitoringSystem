using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Group;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Groups, GetGroupsAsync.DTOs.Group>()
            .ForMember(dest => dest.Payers, opt => opt.MapFrom(src => src.Payers.Select(u => new GetGroupsAsync.DTOs.Payers
            {
                FullName = u.Fullname,
                Id = u.Id 
            })));

    }
}