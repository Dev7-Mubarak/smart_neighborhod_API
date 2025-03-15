namespace OurProjectSmartNeiborhood.Entites
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int MemberId { get; set; }
        public Person Person { get; set; }
        public DateTime DateOfJoin { get; set; }
        public string MemberJob { get; set; }

    }
}
