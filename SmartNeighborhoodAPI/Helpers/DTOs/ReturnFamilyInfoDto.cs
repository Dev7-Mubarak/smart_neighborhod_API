using SmartNeighborhoodAPI.Helpers.DTOs.Families;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.ProjectFamily;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ReturnFamilyInfoDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public  string? Location { get; set; }
        public required string FamilyNotes { get; set; }
        public int FamilyCategoryId { get; set; }
        public required string FamilyCategoryName { get; set; }
        public int FamilyTypeId { get; set; }
        public required string FamilyTypeName { get; set; }
        public int BlockId { get; set; }
        public required string BlockName { get; set; }
        public HeadOfFamilyDto HeadOfFamily { get; set; }
        public List<ReturnFamilyMemberWithFullInfo> FamilyMembers { get; set; }
        public List<Assistances> Assistances { get; set; }
    }
}
