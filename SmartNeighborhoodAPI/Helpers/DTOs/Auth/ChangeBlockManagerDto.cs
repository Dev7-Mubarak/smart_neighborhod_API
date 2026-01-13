namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class ChangeManagerDto
    {
        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "PersonId is required")]
        public int PersonId { get; set; }
    }
}
