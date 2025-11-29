using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

public class GetMedicineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MedicineType Type { get; set; }
    public string? Description { get; set; }

    public static GetMedicineDto Create(Medicine m) => new()
    {
        Id = m.Id,
        Name = m.Name,
        Type = m.Type,
        Description = m.Description
    };
}