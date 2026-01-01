using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ChangeResidentialManagerDto
    {
        [Required]
        public int PersonId { get; set; } 
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int neighborhoodId { get; set; }
    }
}
