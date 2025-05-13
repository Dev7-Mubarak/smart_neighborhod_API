using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Entites
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
