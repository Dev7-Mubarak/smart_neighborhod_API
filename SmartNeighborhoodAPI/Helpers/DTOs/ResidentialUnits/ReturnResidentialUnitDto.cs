using SmartNeighborhoodAPI.Helpers.DTOs.block;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class ReturnResidentialUnitDto
    {
        [SwaggerSchema("Unique ID of the unit")]
        public int Id { get; set; }

        [SwaggerSchema("Name of the residential unit")]
        public string Name { get; set; }

        [SwaggerSchema("ID of the user assigned as Unit Manager")]
        public string UnitManagerId { get; set; }

        [SwaggerSchema("Name of the Unit Manager")]
        public string UnitManagerName { get; set; }

        [SwaggerSchema("Blocks under this unit")]
        public List<BlockSummaryDto> Blocks { get; set; } = new();
    }
}
