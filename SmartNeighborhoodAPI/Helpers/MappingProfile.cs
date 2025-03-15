using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Ad, AdDto>().ReverseMap();
        CreateMap<Complain, ComplainDTo>().ReverseMap();
        CreateMap<ComplainType, ComplainTypeDto>().ReverseMap();

    }
}
