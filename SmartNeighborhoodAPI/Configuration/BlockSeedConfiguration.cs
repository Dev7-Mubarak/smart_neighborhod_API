using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class BlockSeedConfiguration 
        : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            // Seed 20 Blocks for testing the endpoint GET /api/residential-units/{id}/blocks
            builder.HasData(
                new Block { Id = 1, Name = "مبنى 1", ResidentialUnitId = 1, BlockManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd" },
                new Block { Id = 2, Name = "مبنى 2", ResidentialUnitId = 1, BlockManagerId = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee" },
                new Block { Id = 3, Name = "مبنى 3", ResidentialUnitId = 1, BlockManagerId = "ffffffff-ffff-ffff-ffff-ffffffffffff" },
                new Block { Id = 4, Name = "مبنى 4", ResidentialUnitId = 1, BlockManagerId = "10101010-1010-1010-1010-101010101010" },
                new Block { Id = 5, Name = "مبنى 5", ResidentialUnitId = 1, BlockManagerId = "20202020-2020-2020-2020-202020202020" },
                new Block { Id = 6, Name = "مبنى 6", ResidentialUnitId = 1, BlockManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd" },
                new Block { Id = 7, Name = "مبنى 7", ResidentialUnitId = 1, BlockManagerId = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee" },
                new Block { Id = 8, Name = "مبنى 8", ResidentialUnitId = 1, BlockManagerId = "ffffffff-ffff-ffff-ffff-ffffffffffff" },
                new Block { Id = 9, Name = "مبنى 9", ResidentialUnitId = 1, BlockManagerId = "10101010-1010-1010-1010-101010101010" },
                new Block { Id = 10, Name = "مبنى 10", ResidentialUnitId = 1, BlockManagerId = "20202020-2020-2020-2020-202020202020" },
                new Block { Id = 11, Name = "مبنى 11", ResidentialUnitId = 1, BlockManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd" },
                new Block { Id = 12, Name = "مبنى 12", ResidentialUnitId = 1, BlockManagerId = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee" },
                new Block { Id = 13, Name = "مبنى 13", ResidentialUnitId = 1, BlockManagerId = "ffffffff-ffff-ffff-ffff-ffffffffffff" },
                new Block { Id = 14, Name = "مبنى 14", ResidentialUnitId = 1, BlockManagerId = "10101010-1010-1010-1010-101010101010" },
                new Block { Id = 15, Name = "مبنى 15", ResidentialUnitId = 1, BlockManagerId = "20202020-2020-2020-2020-202020202020" },
                new Block { Id = 16, Name = "مبنى 16", ResidentialUnitId = 1, BlockManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd" },
                new Block { Id = 17, Name = "مبنى 17", ResidentialUnitId = 1, BlockManagerId = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee" },
                new Block { Id = 18, Name = "مبنى 18", ResidentialUnitId = 1, BlockManagerId = "ffffffff-ffff-ffff-ffff-ffffffffffff" },
                new Block { Id = 19, Name = "مبنى 19", ResidentialUnitId = 1, BlockManagerId = "10101010-1010-1010-1010-101010101010" },
                new Block { Id = 20, Name = "مبنى 20", ResidentialUnitId = 1, BlockManagerId = "20202020-2020-2020-2020-202020202020" }
            );
        }
    }
}
