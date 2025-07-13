namespace SmartNeighborhoodAPI.Helpers.DTOs.FamilyMember
{
    public class MemberFamilyRoleDto
    {
        [Required(ErrorMessage ="Name is reqired")]
        [MaxLength(40)]
        public string RoleName { get; set; }

        public int Id { get; set; }
    }
}
