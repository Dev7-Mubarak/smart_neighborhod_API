namespace OurProjectSmartNeiborhood.Entites
{
    public class Complain
    {
        public int Id { get; set; }
  

        public byte[] Image { get; set; }
        public string Outcome { get; set; }
        public int ComplainTypeId { get; set; }
        public string Notes { get; set; }
        public ComplainType ComplainType { get; set; }
        public DateTime SessionDate { get; set; }
        public List<PersonComplain> PersonComplains { get; set; }
        public bool IsResolved { get; set; }
    }

}
