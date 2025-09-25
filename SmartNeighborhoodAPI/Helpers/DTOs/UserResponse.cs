using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public DateTime ExpireOn { get; set; }
    }
}
