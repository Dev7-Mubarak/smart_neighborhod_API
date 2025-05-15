namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ReturnFamilyInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string FamilyNotes { get; set; }
        public int FamilyCatgoryId { get; set; }
        public string FamilyCatgoryName { get; set; } 
        public int FamilyTypeId { get; set; }
        public string FamilyTypeName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public int HeadOfTheFamilyId { get; set; }
        public string HeadOfTheFamilyName { get; set; }
        public List<FamilyMemberDto> FamilyMembers { get; set; }
    }
}
