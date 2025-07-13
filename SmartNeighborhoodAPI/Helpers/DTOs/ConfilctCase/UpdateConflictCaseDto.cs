namespace SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase
{
    public class UpdateConflictCaseDto
    {
 
        public int ConflictTypeId { get; set; }
        public string ManagerId { get; set; }
        public int FirstPartyId { get; set; }
        public int SecondPartyId { get; set; }
        public string? Notes { get; set; }
        public IFormFile? Image { get; set; } 
        public DateTime SessionDate { get; set; }
        public bool IsResolved { get; set; }
    }
}
