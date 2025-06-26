namespace OurProjectSmartNeiborhood.Entites
{
    public class Conflict
    {
        public int Id { get; set; }
        public string ManagerId { get; set; }
        public string? Notes { get; set; }
        public bool IsResolved { get; set; }
        public int ConflictTypeId { get; set; }
        public ConflictType ConflictType { get; set; }
        public List<ConfilctParty>? PersonComplains { get; set; }
    }

}
