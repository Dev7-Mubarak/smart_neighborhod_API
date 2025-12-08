using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class AddResidentialUnitDto
    {
        [Required]
        [SwaggerSchema("Name of the residential unit", Nullable = false)]
        public string Name { get; set; }

        [Required]
        [SwaggerSchema("ID of the user assigned as Unit Manager", Nullable = false)]
        public string UnitManagerId { get; set; }
        public List<AddBlockDto>? Blocks { get; set; }
    }
}
