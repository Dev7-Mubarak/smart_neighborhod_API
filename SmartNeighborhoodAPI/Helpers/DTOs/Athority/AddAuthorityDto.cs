using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class AddAuthorityDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
    }
}
