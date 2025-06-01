using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [PasswordPropertyText(true)]
        public string Password { get; set; }
    }
}
