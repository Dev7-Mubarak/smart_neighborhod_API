namespace SmartNeighborhoodAPI.Helpers.DTOs.Families
{
    public class CustomFamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int FamilyCatgoryId { get; set; }
        public string FamilyCatgoryName { get; set; }
        public int FamilyTypeId { get; set; }
        public string FamilyTypeName { get; set; }
        public int BlockId { get; set; }
        public string BlockName { get; set; }
  
        public string PhoneNumber { get; set; }
        public string FamilyNotes { get; set; }
    }
}
