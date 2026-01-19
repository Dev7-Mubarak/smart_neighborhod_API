using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits
{
    public class ReturnUnitBlocksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UnitManagerId { get; set; } = string.Empty;
        public string UnitManagerName { get; set; } = string.Empty;
        public IEnumerable<BlockSummaryDto> Blocks { get; set; } = new List<BlockSummaryDto>();
    }

    public class BlockSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BlockManagerId { get; set; } = string.Empty;
        public string BlockManagerName { get; set; } = string.Empty;
    }
}
