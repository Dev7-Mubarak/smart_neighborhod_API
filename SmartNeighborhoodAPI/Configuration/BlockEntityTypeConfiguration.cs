using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class BlockEntityTypeConfiguration : IEntityTypeConfiguration<Block>
    {
        // Matches the BlockManager seeded in AppUserSeedConfiguration
        private const string BlockManagerId = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb";

        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(b => b.Families)
                .WithOne(f => f.Block)
                .HasForeignKey(f => f.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed: حي الثوره
            builder.HasData(new
            {
                Id = 1,
                Name = "مربع 1",
                ResidentialUnitId = 1,
                BlockManagerId
            },
            new
            {
                Id = 2,
                Name = "مربع 2",
                ResidentialUnitId = 1,
                BlockManagerId
            },
               new
               {
                   Id = 3,
                   Name = "مربع 3",
                   ResidentialUnitId = 1,
                   BlockManagerId
               }
            );
        }
    }
}
