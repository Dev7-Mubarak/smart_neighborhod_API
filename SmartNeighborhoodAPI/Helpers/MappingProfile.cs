using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Ad, AdDto>().ReverseMap();
        CreateMap<Complain, ComplainDTo>().ReverseMap();
        CreateMap<ComplainType, ComplainTypeDto>().ReverseMap();
        CreateMap<ConfilctParty, ConfilctPartyDto>().ReverseMap();
        CreateMap<ProjectCatogory, ProjectCatgoryDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<ProjectFamily, ProjectFamilyDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
        CreateMap<Person, CreatePersonDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Family, FamilyDto>().ReverseMap();
        CreateMap<FamilyMember, FamilyMemberDto>().ReverseMap();


    }
}
