using AutoMapper;

namespace MineralWaterMonitoring.Features.Contributions;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Domain.Contributions, GetContributionsAsync.GetContributionsAsyncResult>()
            .ForMember(dest => dest.PayerName, opt => opt.MapFrom(src => src.Payer.Fullname))
            .ForMember(dest => dest.PayerId, opt => opt.MapFrom(src => src.PayerId));
    }
}