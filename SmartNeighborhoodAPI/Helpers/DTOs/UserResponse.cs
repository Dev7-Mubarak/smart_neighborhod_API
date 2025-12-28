using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public DateTime ExpireOn { get; set; }
    }
}
