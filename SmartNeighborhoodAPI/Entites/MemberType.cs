namespace OurProjectSmartNeiborhood.Entites
{
    public class MemberType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FamilyMember>? FamilyMembers { get; set; }
    }
}
