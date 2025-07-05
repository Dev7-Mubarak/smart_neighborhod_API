using SmartNeighborhoodAPI.Helpers.DTOs.Families;

namespace SmartNeighborhoodAPI.Helpers.DTOs.Project
{
    public class BeneficiaryFamilies
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public List<FamilyDetailsDto> Families { get; set; }
    }
}
