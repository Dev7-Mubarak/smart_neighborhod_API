using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
