using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System.Linq.Expressions;

public class GetAllMedicineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MedicineType Type { get; set; }

    public static readonly Expression<Func<Medicine, GetAllMedicineDto>> Selector =
        m => new GetAllMedicineDto
        {
            Id = m.Id,
            Name = m.Name,
            Type = m.Type
        };
}