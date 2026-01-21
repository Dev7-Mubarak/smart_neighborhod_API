using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ResidentialUnitSeedConfiguration 
        : IEntityTypeConfiguration<ResidentialUnit>
    {
        public void Configure(EntityTypeBuilder<ResidentialUnit> builder)
        {
            // Seed data for testing - 1 Residential Unit
            builder.HasData(
                new ResidentialUnit
                {
                    Id = 1,
                    Name = "الوحدة السكنية 1",
                    ResidentialNeighborhoodId = 1,
                    UnitManagerId = "cccccccc-cccc-cccc-cccc-cccccccccccc"
                }
            );
        }
    }
}
