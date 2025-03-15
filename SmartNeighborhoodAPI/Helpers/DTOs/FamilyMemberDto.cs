using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class FamilyMemberDto
    {
        public int FamilyId { get; set; }
        public int MemberTypeId { get; set; }
    }
}

