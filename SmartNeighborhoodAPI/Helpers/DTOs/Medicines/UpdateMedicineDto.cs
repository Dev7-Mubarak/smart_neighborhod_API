using SmartNeighborhoodAPI.Entites.Enums;

public class UpdateMedicineDto
{
    [Required]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public MedicineType Type { get; set; } = MedicineType.Unknown;
    public string? Description { get; set; }
}