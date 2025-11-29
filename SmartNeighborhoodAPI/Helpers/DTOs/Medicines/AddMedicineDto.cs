using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System.Linq.Expressions;

public class AddMedicineDto
{
    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public MedicineType Type { get; set; } = MedicineType.Unknown;
    public string? Description { get; set; }
}





