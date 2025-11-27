
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Type)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(500);

            builder.HasMany(m => m.Batches)
                .WithOne(b => b.Medicine)
                .HasForeignKey(b => b.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Seed Data
            builder.HasData(
                new Medicine { Id = 1, Name = "Insulin", Type = MedicineType.Diabetes, Description = "For diabetic patients" },
                new Medicine { Id = 2, Name = "Amlodipine", Type = MedicineType.Hypertension, Description = "Blood pressure treatment" },

                new Medicine { Id = 1, Name = "Insulin", Type = MedicineType.Diabetes },
                new Medicine { Id = 2, Name = "Metformin", Type = MedicineType.Diabetes },
                new Medicine { Id = 3, Name = "Amlodipine", Type = MedicineType.Hypertension },
                new Medicine { Id = 4, Name = "Losartan", Type = MedicineType.Hypertension },
                new Medicine { Id = 5, Name = "Antibiotic A", Type = MedicineType.Antibiotic },
                new Medicine { Id = 6, Name = "Painkiller A", Type = MedicineType.Painkiller },
                new Medicine { Id = 7, Name = "Dolo 500", Type = MedicineType.Painkiller },
                new Medicine { Id = 8, Name = "Cold Relief", Type = MedicineType.Unknown },
                new Medicine { Id = 9, Name = "Vitamin C", Type = MedicineType.Unknown },
                new Medicine { Id = 10, Name = "Iron Supplement", Type = MedicineType.Unknown },
                new Medicine { Id = 11, Name = "Lisinopril", Type = MedicineType.Hypertension },
                new Medicine { Id = 12, Name = "Glibenclamide", Type = MedicineType.Diabetes },
                new Medicine { Id = 13, Name = "Amoxicillin", Type = MedicineType.Antibiotic },
                new Medicine { Id = 14, Name = "Ibuprofen", Type = MedicineType.Painkiller },
                new Medicine { Id = 15, Name = "Acetaminophen", Type = MedicineType.Painkiller },
                new Medicine { Id = 16, Name = "Multivitamins", Type = MedicineType.Unknown },
                new Medicine { Id = 17, Name = "Magnesium Tablets", Type = MedicineType.Unknown },
                new Medicine { Id = 18, Name = "Zinc Supplements", Type = MedicineType.Unknown },
                new Medicine { Id = 19, Name = "Cough Syrup", Type = MedicineType.Unknown },
                new Medicine { Id = 20, Name = "Stomach Relief", Type = MedicineType.Unknown }
);

            );
        }
    }
}
