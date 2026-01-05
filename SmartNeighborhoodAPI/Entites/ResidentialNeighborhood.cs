using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;

namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialNeighborhood
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string NeighborhoodManagerId { get; set; } = string.Empty;
        public AppUser NeighborhoodManager { get; set; } = null!;

        public ICollection<ResidentialUnit> ResidentialUnits { get; set; }
            = new List<ResidentialUnit>();

        // Factory → DTO
        public ReturnResidentialNeighborhoodDto ToDto()
        {
            return new ReturnResidentialNeighborhoodDto
            {
                Id = Id,
                Name = Name,
                NeighborhoodManagerId = NeighborhoodManagerId,
                NeighborhoodManagerName = NeighborhoodManager?.Person?.FullName ?? string.Empty,
                Units = ResidentialUnits.Select(u => u.ToDto()).ToList()
            };
        }
    }
}
