namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class BlockWithStatsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public int TotalFamilies { get; set; }
        public int TotalWidows { get; set; }
        public int totalOrphans { get; set; }
    }
}
