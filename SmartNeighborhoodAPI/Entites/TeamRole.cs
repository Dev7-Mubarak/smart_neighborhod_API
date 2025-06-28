namespace SmartNeighborhoodAPI.Entites
{
    public class TeamRole
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public ICollection<TeamMember> TeamMembers { get; set; }
    }

}
