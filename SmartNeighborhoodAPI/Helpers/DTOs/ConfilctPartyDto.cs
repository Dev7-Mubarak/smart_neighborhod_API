namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ConfilctPartyDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
