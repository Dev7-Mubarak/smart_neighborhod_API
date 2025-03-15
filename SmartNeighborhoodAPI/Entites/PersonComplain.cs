namespace OurProjectSmartNeiborhood.Entites
{
    public class PersonComplain
    {
        public int Id { get; set; }

        public int PesonId { get; set; }

        public Person Person { get; set; }
        public int ComplainId { get; set; }
        public Complain Complain { get; set; }

        public int ConfilctPartyId { get; set; }
        public ConfilctParty ConfilctParty { get; set; }



    }

}
