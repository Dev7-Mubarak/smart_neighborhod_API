namespace OurProjectSmartNeiborhood.Entites
{
    public class MemberFamilyRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<FamilyMember> FamilyMembers { get; set; }
    }
}
