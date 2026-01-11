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
            // Seed 5 Blocks for testing the endpoint GET /api/residential-units/{id}/blocks
            builder.HasData(
                new Block
                {
                    Id = 1,
                    Name = "«·„—»⁄ «·√Ê·",
                    ResidentialUnitId = 1,
                    BlockManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd"
                },
                new Block
                {
                    Id = 2,
                    Name = "«·„—»⁄ «·À«‰Ì",
                    ResidentialUnitId = 1,
                    BlockManagerId = "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"
                },
                new Block
                {
                    Id = 3,
                    Name = "«·„—»⁄ «·À«·À",
                    ResidentialUnitId = 1,
                    BlockManagerId = "ffffffff-ffff-ffff-ffff-ffffffffffff"
                },
                new Block
                {
                    Id = 4,
                    Name = "«·„—»⁄ «·—«»⁄",
                    ResidentialUnitId = 1,
                    BlockManagerId = "10101010-1010-1010-1010-101010101010"
                },
                new Block
                {
                    Id = 5,
                    Name = "«·„—»⁄ «·Œ«„”",
                    ResidentialUnitId = 1,
                    BlockManagerId = "20202020-2020-2020-2020-202020202020"
                }
            );
        }
    }
}
