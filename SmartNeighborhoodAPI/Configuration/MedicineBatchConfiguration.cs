using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Models;
using SmartNeighborhoodAPI.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class MedicineBatchConfiguration : IEntityTypeConfiguration<MedicineBatch>
    {
        public void Configure(EntityTypeBuilder<MedicineBatch> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Provider)
                .HasMaxLength(250);

            builder.HasOne(b => b.Medicine)
                .WithMany(m => m.Batches)
                .HasForeignKey(b => b.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new MedicineBatch
                {
                    Id = 1,
                    MedicineId = 1,
                    Quantity = 120,
                    ArrivalDate = new DateTime(2024, 1, 15),
                    ExpireDate = new DateTime(2025, 1, 15),
                    Provider = "Health Association A"
                },
                new MedicineBatch
                {
                    Id = 2,
                    MedicineId = 2,
                    Quantity = 200,
                    ArrivalDate = new DateTime(2024, 2, 1),
                    ExpireDate = new DateTime(2025, 2, 1),
                    Provider = "Medical Charity B"
                }
            );
        }
    }
}
