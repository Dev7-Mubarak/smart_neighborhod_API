using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Helpers.DTOs
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        [MaxLength(30)]
        public string? Job { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string? Image { get; set; }
        public string BloodType { get; set; }
        [MaxLength(100)]
        public string IdentityNumber { get; set; }
        public string? TypeOfIdentity { get; set; }
        [MaxLength(30)]
        public string Status { get; set; }
        public string MemberTypeName { get; set; }
    }
}

