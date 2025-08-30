namespace SmartNeighborhoodAPI.Helpers
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInDays { get; set; }
        public string SigningKey { get; set; }
    }
}
