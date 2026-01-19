using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class AddResidentialUnitDto
    {
        [Required]
        [SwaggerSchema("Name of the residential unit", Nullable = false)]
        public string Name { get; set; }

        [Required]
        [SwaggerSchema("ID of the residential neighborhood this unit belongs to", Nullable = false)]
        public int ResidentialNeighborhoodId { get; set; }

        [Required]
        [SwaggerSchema("ID of the person to be assigned as Unit Manager", Nullable = false)]
        public int PersonId { get; set; }

        [Required]
        [SwaggerSchema("Identifier (email or username) for the Unit Manager account", Nullable = false)]
        public string Identifier { get; set; }

        [Required]
        [SwaggerSchema("Password for the Unit Manager account", Nullable = false)]
        public string Password { get; set; }
    }
}
