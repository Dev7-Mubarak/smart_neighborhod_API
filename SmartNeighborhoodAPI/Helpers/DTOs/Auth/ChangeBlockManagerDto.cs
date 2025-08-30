namespace SmartNeighborhoodAPI.Helpers.DTOs.Auth
{
    public class ChangeBlockManagerDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "PersonId is required")]
        public int PersonId { get; set; }
    }
}
