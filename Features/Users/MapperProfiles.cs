using AutoMapper;

namespace MineralWaterMonitoring.Features.Users;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Domain.Users, GetUsersAsync.UsersAsyncQueryResult>();
    }
}