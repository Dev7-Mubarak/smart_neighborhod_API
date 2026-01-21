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
            // Seed data for testing - 20 Residential Neighborhoods
            builder.HasData(
                new ResidentialNeighborhood { Id = 1, Name = "حي 22 مايو", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 2, Name = "حي أكتوبر", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 3, Name = "حي ابن سيناء", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 4, Name = "حي الثورة", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 5, Name = "حي الحرشيات", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 6, Name = "حي السلام", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 7, Name = "حي الشهيد خالد", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 8, Name = "حي الصديق", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 9, Name = "حي الصيادين", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 10, Name = "حي العمال", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 11, Name = "حي العيص", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 12, Name = "حي المتضررين", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 13, Name = "حي النصر", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 14, Name = "حي امبيخة", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 15, Name = "حي بويش", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 16, Name = "حي خلف", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 17, Name = "حي روكب", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 18, Name = "حي فوه القديمة", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 19, Name = "حي نوفمبر", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" },
                new ResidentialNeighborhood { Id = 20, Name = "مربع واحد", NeighborhoodManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" }
            );
        }
    }
}
