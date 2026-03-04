using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class ProjectFamilyEntityTypeConfiguration : IEntityTypeConfiguration<ProjectFamily>
    {
        public void Configure(EntityTypeBuilder<ProjectFamily> builder)
        {
            builder.HasKey(pf => pf.Id);

            builder.HasOne(pf => pf.Project)
                .WithMany(p => p.ProjectFamilies)
                .HasForeignKey(pf => pf.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pf => pf.Family)
                .WithMany(f => f.ProjectFamilies)
                .HasForeignKey(pf => pf.FamilyID)
                .OnDelete(DeleteBehavior.Cascade);

            // Project 2: حملة نظافة الحي — enrols all 5 families
            builder.HasData(
                new { Id = 1, ProjectID = 2, FamilyID = 1, dateReceived = new DateTime(2025, 3, 1) },
                new { Id = 2, ProjectID = 2, FamilyID = 2, dateReceived = new DateTime(2025, 3, 1) },
                new { Id = 3, ProjectID = 2, FamilyID = 3, dateReceived = new DateTime(2025, 3, 1) },
                new { Id = 4, ProjectID = 2, FamilyID = 4, dateReceived = new DateTime(2025, 3, 1) },
                new { Id = 5, ProjectID = 2, FamilyID = 5, dateReceived = new DateTime(2025, 3, 1) },

                // Project 3: تطوير ملعب الحي — enrols 3 families
                new { Id = 6, ProjectID = 3, FamilyID = 1, dateReceived = new DateTime(2025, 4, 15) },
                new { Id = 7, ProjectID = 3, FamilyID = 3, dateReceived = new DateTime(2025, 4, 15) },
                new { Id = 8, ProjectID = 3, FamilyID = 5, dateReceived = new DateTime(2025, 4, 15) }
            );
        }
    }
}
