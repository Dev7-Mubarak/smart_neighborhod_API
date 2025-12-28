using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class UpdateResidentialNeighborhoodDto : CreateResidentialNeighborhoodDto
    {
        [Required]
        public int Id { get; set; }
    }
}
