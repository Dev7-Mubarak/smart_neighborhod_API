using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class AddResidentialUnitDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int ResidentialNeighborhoodId { get; set; }

        [Required]
        public int UnitManagerId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [SwaggerSchema("Password for the manager account", Nullable = false)]
        public string Password { get; set; }
    }
}
