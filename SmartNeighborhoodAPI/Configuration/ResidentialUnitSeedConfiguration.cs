using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ResidentialUnitSeedConfiguration : IEntityTypeConfiguration<ResidentialUnit>
    {
        // Matches the UnitManager seeded in AppUserSeedConfiguration
        private const string UnitManagerId = "cccccccc-cccc-cccc-cccc-cccccccccccc";

        public void Configure(EntityTypeBuilder<ResidentialUnit> builder)
        {
            // Seed: الوحدة الأولى — parent unit for حي الثوره block
            builder.HasData(new
            {
                Id = 1,
                Name = " وحدة الشفيع",
                ResidentialNeighborhoodId = 1,
                UnitManagerId
            });
        }
    }
}
