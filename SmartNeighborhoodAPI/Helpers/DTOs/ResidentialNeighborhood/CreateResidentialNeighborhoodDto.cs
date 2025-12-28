namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class CreateResidentialNeighborhoodDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int NeighborhoodManagerId { get; set; }
    }



}
