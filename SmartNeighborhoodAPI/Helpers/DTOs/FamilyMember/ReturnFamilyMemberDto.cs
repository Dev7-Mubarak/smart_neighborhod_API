namespace SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember
{
    public class ReturnFamilyMemberDto
    {
        public int PersonId { get; set; }
        public string PersonFullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
