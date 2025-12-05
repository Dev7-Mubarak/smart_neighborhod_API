using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class AddGovernmentInstitutionsDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
    }
}
