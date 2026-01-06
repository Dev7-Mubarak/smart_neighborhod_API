using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class ChangeResidentialUnitManagerDto
    {
        [Required]
        public int unitId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
