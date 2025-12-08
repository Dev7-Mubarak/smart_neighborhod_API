namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class AddBlockDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string BlockManagerId { get; set; } = string.Empty;
    }

}
