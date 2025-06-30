namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ProjectFamilyDtoDetails
    {
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public BlockDetails Block { get; set; }
    }
}
