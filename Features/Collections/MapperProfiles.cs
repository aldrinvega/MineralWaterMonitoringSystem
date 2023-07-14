using AutoMapper;
using MineralWaterMonitoring.Domain;

namespace MineralWaterMonitoring.Features.Collections;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Collection, GetCollectionsAsync.GetCollectionsAsyncQueryResult>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Groups.GroupName))
            .ForMember(dest => dest.CollectedAmount, opt => opt.MapFrom(src => src.Groups.Payers
                .SelectMany(payer => payer.Contributions)
                .Where(contribution => contribution.CollectionId == src.Id)
                .Sum(contribution => contribution.ContributionAmount)));
    }
}