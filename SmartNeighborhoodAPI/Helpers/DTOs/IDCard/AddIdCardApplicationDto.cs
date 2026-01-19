using System.ComponentModel.DataAnnotations;

public class AddIdCardApplicationDto
{
    [Required]
    public int ResidentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ApplicationNumber { get; set; } = string.Empty;

    public string? Notes { get; set; }
}
