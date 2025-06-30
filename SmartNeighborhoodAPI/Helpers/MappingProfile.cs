using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Ad, AdDto>().ReverseMap();
        CreateMap<Complain, ComplainDTo>().ReverseMap();
        CreateMap<ComplainType, ComplainTypeDto>().ReverseMap();
        CreateMap<ConfilctParty, AddConfilctPartyDto>().ReverseMap();
        CreateMap<ProjectCatogory, ProjectCatgoryDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<ProjectFamily, ProjectFamilyDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
        CreateMap<Person, CreatePersonDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Family, FamilyDto>().ReverseMap();
        CreateMap<FamilyMember, FamilyMemberDto>().ReverseMap();
        CreateMap<Project, ProjectDetailsDto>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProjectCatogory.Name));

        CreateMap<Team, TeamDtoDetails>();

        CreateMap<TeamMember, TeamMemberDetails>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Person.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Person.Email));

        CreateMap<ProjectFamily, ProjectFamilyDtoDetails>()
            .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.Family.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Family.FamilyCatgory.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Family.FamilyType.Name))
            .ForMember(dest => dest.Block, opt => opt.MapFrom(src => src.Block));

        CreateMap<Block, BlockDetails>();


    }
}
