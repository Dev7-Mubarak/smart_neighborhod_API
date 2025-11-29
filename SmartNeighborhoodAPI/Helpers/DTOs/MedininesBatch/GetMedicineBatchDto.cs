
using SmartNeighborhoodAPI.Entites;

public class GetMedicineBatchDto
{
    public int Id { get; set; }
    public int MedicineId { get; set; }
    public int Quantity { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? Provider { get; set; }

    public static GetMedicineBatchDto Create(MedicineBatch b) => new()
    {
        Id = b.Id,
        MedicineId = b.MedicineId,
        Quantity = b.Quantity,
        ArrivalDate = b.ArrivalDate,
        ExpireDate = b.ExpireDate,
        Provider = b.Provider
    };
}