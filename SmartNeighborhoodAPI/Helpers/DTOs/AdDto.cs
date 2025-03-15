namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class AdDto
    {
        [Required(ErrorMessage ="Created Date is required")]
        public DateTime CreatedTime { get; set; }
        [Required(ErrorMessage = "Created Ads Text is required")]
        [MaxLength(200)]
        public string AdsText { get; set; }
        [Required(ErrorMessage = "Created Group Id is required")]
        public int GroupId { get; set; }
    }
}
