using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialNeighborhood;
using SmartNeighborhoodAPI.Interfaces;

namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialNeighborhood : ISyncable
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string NeighborhoodManagerId { get; set; } = string.Empty;
        public AppUser NeighborhoodManager { get; set; } = null!;

        public ICollection<ResidentialUnit> ResidentialUnits { get; set; }
            = new List<ResidentialUnit>();

        // ISyncable implementation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }

        // Factory → DTO
        public ReturnResidentialNeighborhoodDto ToDto()
        {
            return new ReturnResidentialNeighborhoodDto
            {
                Id = Id,
                Name = Name,
                NeighborhoodManagerId = NeighborhoodManagerId,
                NeighborhoodManagerName = NeighborhoodManager?.Person?.FullName ?? string.Empty,
            };
        }

        public ReturnResidentialUnitDto ToResidentialUnitDto()
        {
            return new ReturnResidentialUnitDto
            {
                Id = Id,
                Name = Name,
                NeighborhoodManagerId = NeighborhoodManagerId,
                NeighborhoodManagerName = NeighborhoodManager?.Person?.FullName ?? string.Empty,
                ResidentialUnits = ResidentialUnits.Select(u => new ResidentialUnitSummaryDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    UnitManagerId = u.UnitManagerId,
                    UnitManagerName = u.UnitManager?.Person.FullName ?? string.Empty,
                    BlockCount = u.Blocks.Count,
                }).ToList(),
            };
        }
    }
}
