using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Collections;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Collection, GetCollectionsAsync.GetCollectionsAsyncQueryResult>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Groups.GroupName));
    }
}