namespace SmartNeighborhoodAPI.Helpers.DTOs.Families
{
    public class HeadOfFamilyDto
    {
        public string IdentityNumber { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
