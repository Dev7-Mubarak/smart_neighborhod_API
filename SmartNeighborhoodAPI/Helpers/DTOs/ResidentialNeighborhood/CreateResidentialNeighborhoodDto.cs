using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class CreateResidentialNeighborhoodDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string NeighborhoodManagerId { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
