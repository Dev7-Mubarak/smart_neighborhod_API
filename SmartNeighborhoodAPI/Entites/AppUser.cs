using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Entites
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        [MaxLength(10)]
        public string? EmailConfirmationCode { get; set; }
        public DateTime? EmailConfirmationCodeExpiresAt { get; set; }
    }
}
