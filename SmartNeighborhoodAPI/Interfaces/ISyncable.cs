using System;

namespace SmartNeighborhoodAPI.Interfaces
{
    public interface ISyncable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }

        // The ClientId is used to identify the client that created or updated the record.
        string? ClientId { get; set; }
    }
}
