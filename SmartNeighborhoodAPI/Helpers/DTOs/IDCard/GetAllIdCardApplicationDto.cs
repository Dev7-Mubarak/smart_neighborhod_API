using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;
using System;
using System.Linq.Expressions;

public class GetAllIdCardApplicationDto
{
    public int Id { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }

    public static readonly Expression<Func<IDCardApplication, GetAllIdCardApplicationDto>> Selector =
        p => new GetAllIdCardApplicationDto
        {
            Id = p.Id,
            ApplicationNumber = p.ApplicationNumber,
            Status = p.Status
        };
}
