using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood
{
    public class ReturnResidentialUnitDto : ReturnResidentialNeighborhoodDto
    {
        public ICollection<ResidentialUnit> ResidentialUnits { get; set; }
    }
}
