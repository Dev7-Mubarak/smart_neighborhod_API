namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class ComplainTypeDto
    {
        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
