namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class BlockDto
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [MaxLength(30)]
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
