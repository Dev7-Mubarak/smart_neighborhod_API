
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class PassportApplicationConfiguration : IEntityTypeConfiguration<PassportApplication>
    {
        public void Configure(EntityTypeBuilder<PassportApplication> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ApplicationNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Status)
                .IsRequired();

  
            builder.Property(p => p.LastUpdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasData(
                new PassportApplication
                {
                    Id = 1,
                    ResidentId = 1,
                    ApplicationNumber = "PAS-1001",
                    Status = ApplicationStatus.InProgress,
                    LastUpdate = DateTime.UtcNow,
                    Notes = "Under review"
                },
                new PassportApplication
                {
                    Id = 2,
                    ResidentId = 2,
                    ApplicationNumber = "PAS-1002",
                    Status = ApplicationStatus.Ready,
                    LastUpdate = DateTime.UtcNow,
                    Notes = "Ready for pickup"
                },
                new PassportApplication { Id = 21, ResidentId = 1, ApplicationNumber = "PAS1001", Status = ApplicationStatus.Received, Notes = "Waiting", LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 22, ResidentId = 2, ApplicationNumber = "PAS1002", Status = ApplicationStatus.InProgress, Notes = "Processing", LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 3, ResidentId = 3, ApplicationNumber = "PAS1003", Status = ApplicationStatus.Ready, Notes = "Ready", LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 4, ResidentId = 4, ApplicationNumber = "PAS1004", Status = ApplicationStatus.Rejected, Notes = "Missing documents", LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 5, ResidentId = 5, ApplicationNumber = "PAS1005", Status = ApplicationStatus.Ready, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 6, ResidentId = 6, ApplicationNumber = "PAS1006", Status = ApplicationStatus.Received, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 7, ResidentId = 7, ApplicationNumber = "PAS1007", Status = ApplicationStatus.InProgress, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 8, ResidentId = 8, ApplicationNumber = "PAS1008", Status = ApplicationStatus.Received, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 9, ResidentId = 9, ApplicationNumber = "PAS1009", Status = ApplicationStatus.InProgress, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 10, ResidentId = 10, ApplicationNumber = "PAS1010", Status = ApplicationStatus.Ready, Notes = null, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 11, ResidentId = 11, ApplicationNumber = "PAS1011", Status = ApplicationStatus.Received, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 12, ResidentId = 12, ApplicationNumber = "PAS1012", Status = ApplicationStatus.Ready, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 13, ResidentId = 13, ApplicationNumber = "PAS1013", Status = ApplicationStatus.InProgress, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 14, ResidentId = 14, ApplicationNumber = "PAS1014", Status = ApplicationStatus.Received, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 15, ResidentId = 15, ApplicationNumber = "PAS1015", Status = ApplicationStatus.Ready, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 16, ResidentId = 16, ApplicationNumber = "PAS1016", Status = ApplicationStatus.Rejected, Notes = "Invalid photo", LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 17, ResidentId = 17, ApplicationNumber = "PAS1017", Status = ApplicationStatus.InProgress, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 18, ResidentId = 18, ApplicationNumber = "PAS1018", Status = ApplicationStatus.Received, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 19, ResidentId = 19, ApplicationNumber = "PAS1019", Status = ApplicationStatus.Ready, LastUpdate = DateTime.UtcNow },
                new PassportApplication { Id = 20, ResidentId = 20, ApplicationNumber = "PAS1020", Status = ApplicationStatus.InProgress, LastUpdate = DateTime.UtcNow }

            );
        }
    }
}
