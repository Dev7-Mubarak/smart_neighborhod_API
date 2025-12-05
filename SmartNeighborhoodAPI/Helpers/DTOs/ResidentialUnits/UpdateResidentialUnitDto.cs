using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class UpdateResidentialUnitDto
    {
        [Required]
        [SwaggerSchema("Updated name of the residential unit", Nullable = false)]
        public string Name { get; set; }

        [SwaggerSchema("Updated Unit Manager ID (optional)")]
        public string? UnitManagerId { get; set; }
    }
}
