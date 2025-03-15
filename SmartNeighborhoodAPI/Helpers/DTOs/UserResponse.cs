namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }
    }
}
