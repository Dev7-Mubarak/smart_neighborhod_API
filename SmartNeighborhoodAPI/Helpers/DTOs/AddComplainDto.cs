namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class AddComplainDto
    {
        [Required(ErrorMessage = "Outcome is required")]
        public string Outcome { get; set; }

        [Required(ErrorMessage = "Complain Type is required")]
        public int ComplainTypeId { get; set; }

        [Required(ErrorMessage = "Notes are required")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "Session Date is required")]
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = "Manager is required")]
        public int ManagerId { get; set; }

        public bool IsResolved { get; set; }

        public IFormFile? Image { get; set; }

        public ConflictPartyDto? FirstParty { get; set; }

        public ConflictPartyDto? SecondParty { get; set; }
    }

}
