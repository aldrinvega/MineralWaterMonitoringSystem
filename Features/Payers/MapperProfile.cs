using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MineralWaterMonitoring.Features.Payers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Domain.Payers, GetPayersAsync.GetPayersAsyncQueryResult>()
            .ForMember(dest => dest.GroupName, opt => opt
                .MapFrom(src => src.Groups.GroupName))
            .ForMember(dest => dest.GroupCode, opt => opt
                .MapFrom(src => src.Groups.GroupCode));
    }
}