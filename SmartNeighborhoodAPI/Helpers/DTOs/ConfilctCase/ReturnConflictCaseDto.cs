namespace SmartNeighborhoodAPI.Helpers.DTOs.ConfilctCase
{
    public class ReturnConflictCaseDto
    {
        public int Id { get; set; }

        public int ConflictTypeId { get; set; }
        public string ConflictTypeName { get; set; }

        public string ManagerId { get; set; }
        public string ManagerName { get; set; }

        public int FirstPartyId { get; set; }
        public string FirstPartyName { get; set; }

        public int SecondPartyId { get; set; }
        public string SecondPartyName { get; set; }

        public string? Notes { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime SessionDate { get; set; }
        public bool IsResolved { get; set; }
    
    }

}
