using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ReturnFamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int FamilyCatgoryId { get; set; }
        public string FamilyNotes { get; set; }
        public string HousingType { get; set; }
        public int FamilyTypeId { get; set; }
        public int BlockId { get; set; }
        public int FamilyMemberId { get; set; }
    }
}
