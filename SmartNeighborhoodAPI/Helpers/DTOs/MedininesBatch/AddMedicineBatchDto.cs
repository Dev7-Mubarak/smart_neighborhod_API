using SmartNeighborhoodAPI.Entites;

public class AddMedicineBatchDto
{
    [Required]
    public int MedicineId { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    public DateTime? ArrivalDate { get; set; }
    public DateTime? ExpireDate { get; set; }

    [MaxLength(250)]
    public string? Provider { get; set; }
}


