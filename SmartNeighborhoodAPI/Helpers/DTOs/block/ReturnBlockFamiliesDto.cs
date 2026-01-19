using SmartNeighborhoodAPI.Helpers.DTOs.Families;

namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class ReturnBlockFamiliesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BlockManagerId { get; set; } = string.Empty;
        public string BlockManagerName { get; set; } = string.Empty;
        public IEnumerable<FamilySummaryDto> Families { get; set; } = new List<FamilySummaryDto>();
    }

    public class FamilySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int FamilyCategoryId { get; set; }
        public string FamilyCategoryName { get; set; } = string.Empty;
    }
}
