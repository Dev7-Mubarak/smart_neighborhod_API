using SmartNeighborhoodAPI.Entites.Enums;
using System.Security.Cryptography.X509Certificates;

namespace SmartNeighborhoodAPI.Entites
{
    public class IDCardApplication
    {
        [Key]
        public int Id { get; set; }

        public int ResidentId { get; set; }
        public Person Resident { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string ApplicationNumber { get; set; } = string.Empty;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Received;

        public DateTime? LastUpdate { get; set; }

        public string? Notes { get; set; }
    }
}
