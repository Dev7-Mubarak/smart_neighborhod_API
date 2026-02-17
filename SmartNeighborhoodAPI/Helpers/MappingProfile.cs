using SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase;
using SmartNeighborhoodAPI.Helpers.DTOs.ConflictType;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.Issue;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;
using SmartNeighborhoodAPI.Helpers.DTOs.Project;
using SmartNeighborhoodAPI.Helpers.DTOs.TeamMembers;
using SmartNeighborhoodAPI.Helpers.DTOs.Sync;
using SmartNeighborhoodAPI.Entites;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

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
            .ForMember(dest => dest.FirstPartyName, opt => opt.MapFrom(src => src.FirstParty.Person.FullName))
            .ForMember(dest => dest.SecondPartyName, opt => opt.MapFrom(src => src.SecondParty.Person.FullName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagePath));
        CreateMap<ConflictCase, ReturnConflictCaseDto>()
    .ForMember(dest => dest.ConflictTypeName, opt => opt.MapFrom(src => src.ConflictType.Name))
    .ForMember(dest => dest.FirstPartyName, opt => opt.MapFrom(src => src.FirstParty.Person.FullName))
    .ForMember(dest => dest.SecondPartyName, opt => opt.MapFrom(src => src.SecondParty.Person.FullName))
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImagePath));

        CreateMap<Issue, IssueDto>().ReverseMap();
        CreateMap<CreateIssueDto, Issue>();
        CreateMap<UpdateIssueDto, Issue>();

        // Sync DTOs mappings
        // Person sync mappings
        CreateMap<PersonChangeDto, Person>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is handled separately
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.Gender) ? (Gender?)null : Enum.Parse<Gender>(src.Gender)))
            .ForMember(dest => dest.BloodType, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.BloodType) ? default(BloodType) : Enum.Parse<BloodType>(src.BloodType)))
            .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.MaritalStatus) ? default(MaritalStatus) : Enum.Parse<MaritalStatus>(src.MaritalStatus)))
            .ForMember(dest => dest.OccupationStatus, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.OccupationStatus) ? default(OccupationStatus) : Enum.Parse<OccupationStatus>(src.OccupationStatus)))
            .ForMember(dest => dest.personType, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.PersonType) ? default(PersonType) : Enum.Parse<PersonType>(src.PersonType)))
            .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.VehicleType) ? (VehicleType?)null : Enum.Parse<VehicleType>(src.VehicleType)))
            .ForMember(dest => dest.ResidencyStatus, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.ResidencyStatus) ? (ResidencyStatus?)null : Enum.Parse<ResidencyStatus>(src.ResidencyStatus)))
            .ForMember(dest => dest.FamilyMembers, opt => opt.Ignore())
            .ForMember(dest => dest.TeamMemberships, opt => opt.Ignore());

        // Family sync mappings
        CreateMap<FamilyChangeDto, Family>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is handled separately
            .ForMember(dest => dest.FamilyCatgoryId, opt => opt.MapFrom(src => int.Parse(src.FamilyCategoryId)))
            .ForMember(dest => dest.BlockId, opt => opt.MapFrom(src => int.Parse(src.BlockId)))
            .ForMember(dest => dest.HousingType, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.HousingType) ? default(HousingType) : Enum.Parse<HousingType>(src.HousingType)))
            .ForMember(dest => dest.FamilyCatgory, opt => opt.Ignore())
            .ForMember(dest => dest.Block, opt => opt.Ignore())
            .ForMember(dest => dest.FamilyMembers, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectFamilies, opt => opt.Ignore());

        // Issue sync mappings
        CreateMap<IssueChangeDto, Issue>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is handled separately
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.Priority) ? default(IssuePriority) : Enum.Parse<IssuePriority>(src.Priority)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.Status) ? default(IssueStatus) : Enum.Parse<IssueStatus>(src.Status)))
            .ForMember(dest => dest.Reporter, opt => opt.Ignore())
            .ForMember(dest => dest.Assignee, opt => opt.Ignore())
            .ForMember(dest => dest.Attachments, opt => opt.Ignore())
            .ForMember(dest => dest.ResolvedAt, opt => opt.Ignore());
    }
}

