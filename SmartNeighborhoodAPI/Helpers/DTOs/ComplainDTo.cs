namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ComplainDTo
    {
        [Required(ErrorMessage ="Image is required")]
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "OutCome is required")]
        [MaxLength(300)]
        public string Outcome { get; set; }
        [Required(ErrorMessage = "OutCome is required")]

        public int ComplainTypeId { get; set; }
  
        [MaxLength(300)]
        public string Notes { get; set; }
        [Required(ErrorMessage = "SessionDate is required")]

        public DateTime SessionDate { get; set; }
        [Required(ErrorMessage = "IsResolved is required")]
        public bool IsResolved { get; set; }
    }
}
