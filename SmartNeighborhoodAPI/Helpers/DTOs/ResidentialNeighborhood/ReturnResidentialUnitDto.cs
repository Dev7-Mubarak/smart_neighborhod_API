using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ReturnResidentialUnitDto : ReturnResidentialNeighborhoodDto
    {
        public IEnumerable<ResidentialUnitSummaryDto> ResidentialUnits { get; set; }
    }
}
