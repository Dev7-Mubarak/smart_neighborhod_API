using System.ComponentModel.DataAnnotations;
using SmartNeighborhoodAPI.Helpers.DTOs.Person;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class FamilyMemberDto
    {
        public int PersonId { get; set; }
        public int FamilyId { get; set; }
        public int RoleId { get; set; }
    }
}

