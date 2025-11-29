using SmartNeighborhoodAPI.Entites.Enums;
using System.ComponentModel.DataAnnotations;

public class UpdateIdCardApplicationDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string ApplicationNumber { get; set; } = string.Empty;

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Received;

    public string? Notes { get; set; }
}
