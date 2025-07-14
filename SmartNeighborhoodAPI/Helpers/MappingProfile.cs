using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<Ad, AdDto>().ReverseMap();
        CreateMap<ProjectCatogory, ProjectCatgoryDto>().ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<ProjectFamily, ProjectFamilyDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<TeamMember, TeamMemberDto>().ReverseMap();
        CreateMap<Person, CreatePersonDto>().ReverseMap();
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Family, FamilyDto>().ReverseMap();
        CreateMap<ConfilctType, AddConflictCaseDto>().ReverseMap();
        CreateMap<ConfilctType, GetConflictTypeDto>().ReverseMap();
        CreateMap<ConfilctType, UpdateConflictTypeDto>().ReverseMap();
        CreateMap<FamilyMember, FamilyMemberDto>().ReverseMap();
   
        CreateMap<AddConflictCaseDto, ConflictCase>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());


        CreateMap<UpdateConflictCaseDto, ConflictCase>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore()); 

        CreateMap<ConflictCase, GetConflictCaseDto>()
            .ForMember(dest => dest.ConflictTypeName, opt => opt.MapFrom(src => src.ConflictType.Name))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Person.FullName))
            .ForMember(dest => dest.FirstPartyName, opt => opt.MapFrom(src => src.FirstParty.Person.FullName))
            .ForMember(dest => dest.SecondPartyName, opt => opt.MapFrom(src => src.SecondParty.Person.FullName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagePath));
        CreateMap<ConflictCase, ReturnConflictCaseDto>()
    .ForMember(dest => dest.ConflictTypeName, opt => opt.MapFrom(src => src.ConflictType.Name))
    .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Person.FullName))
    .ForMember(dest => dest.FirstPartyName, opt => opt.MapFrom(src => src.FirstParty.Person.FullName))
    .ForMember(dest => dest.SecondPartyName, opt => opt.MapFrom(src => src.SecondParty.Person.FullName))
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagePath));


    }
}
