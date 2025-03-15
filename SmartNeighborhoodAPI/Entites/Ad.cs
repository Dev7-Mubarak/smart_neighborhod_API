namespace OurProjectSmartNeiborhood.Entites
{
    public class Ad
    {
        public int Id { get; set; }
       
        public DateTime CreatedTime { get; set; }
        public string AdsText { get; set; }
        public int GroupId { get; set; }
       
        public List<Group> Groups { get; set; }

    }
}
