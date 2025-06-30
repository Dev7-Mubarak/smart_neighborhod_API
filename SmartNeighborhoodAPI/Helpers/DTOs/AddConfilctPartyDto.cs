namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class AddConfilctPartyDto
    {
        public int? PersonId { get; set; } 

        public string? IdentityNumber { get; set; }
        public string? FullName { get; set; }   

        [Required]
        public int ComplainId { get; set; }
    }

}
