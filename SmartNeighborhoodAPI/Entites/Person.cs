using System.Text.Json.Serialization;
using SmartNeighborhoodAPI.Entites.Enums;
using SmartNeighborhoodAPI.Interfaces;

namespace OurProjectSmartNeiborhood.Entites
{
    public class Person : ISyncable
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string SecondName { get; set; }
        [MaxLength(20)]
        public string ThirdName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(30)]
        public string? PhoneNumber { get; set; }
        public bool? IsWhatsapp { get; set; }
        public bool? IsContactNumber { get; set; }
        [MaxLength(100)]
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Image { get; set; }
        public BloodType BloodType { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public OccupationStatus OccupationStatus { get; set; }
        public PersonType personType { get; set; }
        [MaxLength(30)]
        public string? Job { get; set; }

        // National ID (بطاقة شخصية)
        [MaxLength(30)]
        public string? NationalId { get; set; }

        // Vehicle info
        public VehicleType? VehicleType { get; set; }
        [MaxLength(50)]
        public string? VehicleRegistrationNumber { get; set; }

        // Residency status (Resident or Displaced)
        public ResidencyStatus? ResidencyStatus { get; set; }

        // Chronic disease info
        public bool? HasChronicDiseases { get; set; }
        [MaxLength(250)]
        public string? ChronicDiseasesNotes { get; set; }

        public List<FamilyMember> FamilyMembers { get; set; }
        public ICollection<TeamMember> TeamMemberships { get; set; }

        [JsonIgnore]
        public string FullName =>
        $"{FirstName} {SecondName} {ThirdName} {LastName}".Replace("  ", " ").Trim();

        // ISyncable implementation :-
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? ClientId { get; set; }
    }
}
