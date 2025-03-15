namespace OurProjectSmartNeiborhood.Entites
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Project Project { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}
