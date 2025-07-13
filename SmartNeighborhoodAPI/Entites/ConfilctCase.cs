namespace OurProjectSmartNeiborhood.Entites
{
    public class ConflictCase
    {
        public int Id { get; set; }
        public int FirstPartyId { get; set; }
        public int SecondPartyId { get; set; }
        public string ManagerId { get; set; }
        public int ConflictTypeId { get; set; }
        public string Notes { get; set; }
        public string ImagePath { get; set; }
        public DateTime SessionDate { get; set; }
        public bool IsResolved { get; set; }

        public Person FirstParty { get; set; }
        public Person SecondParty { get; set; }
        public Person Manager { get; set; }
        public ConfilctType ConflictType { get; set; }
    }

}
