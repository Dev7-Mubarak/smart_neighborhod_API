using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Entites
{
    public class TeamRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<TeamMember> TeamMembers { get; set; }
    }

}
