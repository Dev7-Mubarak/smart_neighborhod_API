using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Team:SyncEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectTeam> ProjectTeams { get; set; }
        public ICollection<TeamMember> TeamMembers { get; set; }
    }
}
