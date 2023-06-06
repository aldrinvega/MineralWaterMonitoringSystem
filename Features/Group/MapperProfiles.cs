using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Group;

public class MapperProfiles : Profile
{

    public MapperProfiles()
    {
        CreateMap<Groups, GetGroupsAsync.GroupsAsyncQueryResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UsersCollection.Select(x => x.FullName)));
    }
}