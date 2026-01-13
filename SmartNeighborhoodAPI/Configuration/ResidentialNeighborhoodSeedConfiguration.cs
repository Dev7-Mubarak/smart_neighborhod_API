using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ResidentialNeighborhoodSeedConfiguration 
        : IEntityTypeConfiguration<ResidentialNeighborhood>
    {
        public void Configure(EntityTypeBuilder<ResidentialNeighborhood> builder)
        {
            // Seed data for testing - 1 Residential Neighborhood
            builder.HasData(
                new ResidentialNeighborhood
                {
                    Id = 1,
                    Name = "ÕÌ «·”·«„",
                    NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"
                }
            );
        }
    }
}
