using SmartNeighborhoodAPI.Helpers.DTOs.ResidentialUnits;

namespace SmartNeighborhoodAPI.Entites
{
    public class ResidentialUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UnitManagerId { get; set; }
        public AppUser UnitManager { get; set; }

        public ICollection<Block> Blocks { get; set; } = new List<Block>();

        public ReturnResidentialUnitDto ToDto()
        {
            return new ReturnResidentialUnitDto
            {
                Id = this.Id,
                Name = this.Name,
                UnitManagerId = this.UnitManagerId,
                UnitManagerName = this.UnitManager?.UserName ?? string.Empty,
                Blocks = this.Blocks?.Select(b => new BlockSummaryDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    BlockManagerId = b.BlockManagerId,
                }).ToList()
            };
        }
    }
}
