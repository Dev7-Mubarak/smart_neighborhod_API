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
                    BlockManagerId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
                },
                new Block
                {
                    Id = 2,
                    Name = "«·„—»⁄ «·À«‰Ì",
                    ResidentialUnitId = 1,
                    BlockManagerId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
                },
                new Block
                {
                    Id = 3,
                    Name = "«·„—»⁄ «·À«·À",
                    ResidentialUnitId = 1,
                    BlockManagerId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
                },
                new Block
                {
                    Id = 4,
                    Name = "«·„—»⁄ «·—«»⁄",
                    ResidentialUnitId = 1,
                    BlockManagerId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
                },
                new Block
                {
                    Id = 5,
                    Name = "«·„—»⁄ «·Œ«„”",
                    ResidentialUnitId = 1,
                    BlockManagerId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"
                }
            );
        }
    }
}
