namespace SmartNeighborhoodAPI.Helpers.DTOs.block
{
    public class blockManagerRegister
    {
        [Required(ErrorMessage = "User Name is required")]
        [MinLength(4, ErrorMessage = "User Name must be at least 4 characters long")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        public int PersonId { get; set; }
    }
}
