using AutoMapper;

namespace MineralWaterMonitoring.Features.Authenticate;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Domain.Users, AuthenticateUser.AuthenticateUserResult>();
    }
}