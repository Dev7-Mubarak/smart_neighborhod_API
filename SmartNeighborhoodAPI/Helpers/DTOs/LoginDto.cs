using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText(true)]
        public string Password { get; set; }
    }
}
