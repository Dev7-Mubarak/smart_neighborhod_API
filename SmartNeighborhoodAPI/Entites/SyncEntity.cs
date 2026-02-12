using System;
using System.ComponentModel.DataAnnotations;

namespace SmartNeighborhoodAPI.Entites
{

    public abstract class SyncEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  
        public bool IsDeleted { get; set; } = false;

     
        public DateTime? DeletedAt { get; set; }

        [MaxLength(20)]
        public string SyncStatus { get; set; } = "pending";

        [MaxLength(36)]
        public string? ClientId { get; set; }


        public DateTime? LastSyncedAt { get; set; }


        public string? ConflictResolution { get; set; }
    }
}
