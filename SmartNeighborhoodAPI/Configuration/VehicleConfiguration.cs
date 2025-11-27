
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.PlateNumber)
                .HasMaxLength(20);

            builder.Property(v => v.Model)
                .HasMaxLength(50);

            builder.Property(v => v.Type)
                .IsRequired();

            builder.HasOne(v => v.Owner)
                .WithMany(p => p.Vehicles)
                .HasForeignKey(v => v.OwnerResidentId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Seed Data
            builder.HasData(
                new Vehicle
                {
                    Id = 1,
                    PlateNumber = "1234A",
                    OwnerResidentId = 1,
                    Model = "Toyota Camry",
                    Type = VehicleType.Car,
                    LicenseExpiryDate = new DateTime(2025, 01, 10)
                },
                new Vehicle
                {
                    Id = 2,
                    PlateNumber = "8891M",
                    OwnerResidentId = 2,
                    Model = "Honda Motorcycle",
                    Type = VehicleType.Motorcycle,
                    LicenseExpiryDate = new DateTime(2024, 08, 20)
                },
                new Vehicle { Id = 21, PlateNumber = "A1234", OwnerResidentId = 1, Model = "Toyota Camry", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 1, 1) },
                new Vehicle { Id = 22, PlateNumber = "B5678", OwnerResidentId = 2, Model = "Hyundai Tucson", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2024, 12, 20) },
                new Vehicle { Id = 3, PlateNumber = "C9981", OwnerResidentId = 3, Model = "Nissan Patrol", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 4, 14) },
                new Vehicle { Id = 4, PlateNumber = "D1122", OwnerResidentId = 4, Model = "Kia Sportage", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 10, 9) },
                new Vehicle { Id = 5, PlateNumber = "M7788", OwnerResidentId = 5, Model = "Honda Civic", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 8, 16) },
                new Vehicle { Id = 6, PlateNumber = "X1010", OwnerResidentId = 6, Model = "Honda Motorcycle", Type = VehicleType.Motorcycle, LicenseExpiryDate = new DateTime(2024, 6, 1) },
                new Vehicle { Id = 7, PlateNumber = "P2211", OwnerResidentId = 7, Model = "Suzuki Motorcycle", Type = VehicleType.Motorcycle, LicenseExpiryDate = new DateTime(2024, 11, 30) },
                new Vehicle { Id = 8, PlateNumber = "T5544", OwnerResidentId = 8, Model = "Ford Ranger", Type = VehicleType.Truck, LicenseExpiryDate = new DateTime(2025, 2, 15) },
                new Vehicle { Id = 9, PlateNumber = "V9090", OwnerResidentId = 9, Model = "Isuzu D-Max", Type = VehicleType.Truck, LicenseExpiryDate = new DateTime(2025, 3, 10) },
                new Vehicle { Id = 10, PlateNumber = null, OwnerResidentId = 10, Model = "Unknown Motorcycle", Type = VehicleType.Motorcycle, LicenseExpiryDate = null },
                new Vehicle { Id = 11, PlateNumber = "U3401", OwnerResidentId = 11, Model = "Toyota Corolla", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 7, 22) },
                new Vehicle { Id = 12, PlateNumber = "K4412", OwnerResidentId = 12, Model = "Hyundai Elantra", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2026, 1, 5) },
                new Vehicle { Id = 13, PlateNumber = "Q9971", OwnerResidentId = 13, Model = "Mazda 6", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2024, 9, 11) },
                new Vehicle { Id = 14, PlateNumber = "L5582", OwnerResidentId = 14, Model = "Chevrolet Spark", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 2, 2) },
                new Vehicle { Id = 15, PlateNumber = "H8812", OwnerResidentId = 15, Model = "Toyota Yaris", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2024, 10, 19) },
                new Vehicle { Id = 16, PlateNumber = "S9420", OwnerResidentId = 16, Model = "Honda CRV", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 9, 30) },
                new Vehicle { Id = 17, PlateNumber = "J3391", OwnerResidentId = 17, Model = "Toyota Hilux", Type = VehicleType.Truck, LicenseExpiryDate = new DateTime(2024, 12, 3) },
                new Vehicle { Id = 18, PlateNumber = "R7310", OwnerResidentId = 18, Model = "Suzuki Alto", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2025, 8, 18) },
                new Vehicle { Id = 19, PlateNumber = "W4410", OwnerResidentId = 19, Model = "BMW 320i", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2026, 2, 14) },
                new Vehicle { Id = 20, PlateNumber = "Z5501", OwnerResidentId = 20, Model = "Mercedes C200", Type = VehicleType.Car, LicenseExpiryDate = new DateTime(2026, 5, 20) }

            );
        }
    }
}
