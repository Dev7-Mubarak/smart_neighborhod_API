using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Entites
{
    public class ConfilctParty
    {
        public int Id { get; set; }
        public int ComplainId { get; set; }
        public Complain Complain { get; set; }
        public int? PersonId { get; set; } // If from area
        public Person? Person { get; set; }
        public int? ExternalPersonId { get; set; } // If out of area
        public ExternalPerson? ExternalPerson { get; set; }
        public bool IsFirstParty { get; set; }
    }

}
