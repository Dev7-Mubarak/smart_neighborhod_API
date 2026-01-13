namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class BlockDashboardDto
    {
        public int TotalBlocks { get; set; }
        public int TotalFamilies { get; set; }
        public List<BlockStatsDto> Blocks { get; set; } = new();
    }

    public class BlockStatsDto
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int FamiliesCount { get; set; }
        public string ManagerId { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
    }
}
