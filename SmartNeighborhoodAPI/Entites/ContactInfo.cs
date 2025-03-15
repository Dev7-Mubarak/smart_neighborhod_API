namespace OurProjectSmartNeiborhood.Entites
{
    public class ContactInfo
    {
        public int Id { get; set; }

        public string PhontNumber { get; set; }
        public bool IsPhoneNumber { get; set; }
        public bool IsWhatsappNumber { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
