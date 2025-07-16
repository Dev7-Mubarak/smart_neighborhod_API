using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember;
using SmartNeighborhoodAPI.Helpers.DTOs.FamilyMembers;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Families
{
    public class ReturnFamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FamilyNotes { get; set; }
        public int FamilyCategoryId { get; set; }
        public string FamilyCategoryName { get; set; }    
        public int FamilyTypeId { get; set; }
        public string FamilyTypeName { get; set; }          
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public List<ReturnFamilyMemberDto> FamilyMembers { get; set; }
    }
}
