namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class CreateBlockManagerDto
    {
       [Required(ErrorMessage = "Email is required")] 
       [EmailAddress]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        public int PersonId { get; set; }
        public int ResitinalUnitId { get; set; }
    }
}
