using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

public class GetPassportApplicationDto
{
    public int Id { get; set; }
    public int ResidentId { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }
    public DateTime? LastUpdate { get; set; }
    public string? Notes { get; set; }

    public static GetPassportApplicationDto Create(PassportApplication entity)
        => new()
        {
            Id = entity.Id,
            ResidentId = entity.ResidentId,
            ApplicationNumber = entity.ApplicationNumber,
            Status = entity.Status,
            LastUpdate = entity.LastUpdate,
            Notes = entity.Notes
        };
}

