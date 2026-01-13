using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class UpdateResidentialNeighborhoodDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
