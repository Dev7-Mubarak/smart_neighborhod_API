namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class BlockDto
    {
        [Required(ErrorMessage = "The Name Is Required")]
        [MaxLength(30)]
        public string Name { get; set; }
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Email is required")] 
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
