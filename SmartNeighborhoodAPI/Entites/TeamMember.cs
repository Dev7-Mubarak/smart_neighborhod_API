using SmartNeighborhoodAPI.Entites;
using static AutoMapper.Internal.ExpressionFactory;

namespace OurProjectSmartNeiborhood.Entites
{
    public class TeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public DateTime DateOfJoin { get; set; }

        public int TeamRoleId { get; set; }
        public TeamRole TeamRole { get; set; }
    }

}
