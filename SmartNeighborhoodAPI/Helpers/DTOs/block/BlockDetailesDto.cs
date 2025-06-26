using SmartNeighborhoodAPI.Helpers.DTOs.Families;

namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class BlockDetailesDto
    {
        public BlockWithStatsDto Block { get; set; }
        public List<FamilyDetailsDto> Families { get; set; }
    }
}
