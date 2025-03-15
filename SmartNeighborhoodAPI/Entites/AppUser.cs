using Microsoft.AspNetCore.Identity;

namespace SmartNeighborhoodAPI.Entites
{
    public class AppUser : IdentityUser
    {
        public bool IsActive { get; set; }
        public int? FamilyMemberId { get; set; }
        public FamilyMember? FamilyMember { get; set; }
    }
}
