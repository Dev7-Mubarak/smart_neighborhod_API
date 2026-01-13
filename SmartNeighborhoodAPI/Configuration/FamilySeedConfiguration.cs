using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Configuration
{
    public class FamilySeedConfiguration 
        : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            // Seed 10 families across the 5 blocks for testing
            builder.HasData(
                // Block 1 - 2 families
                new Family
                {
                    Id = 1,
                    Name = "⁄«∆·… «·√Õ„œÌ",
                    Location = "‘«—⁄ «·„·ﬂ ›Âœ° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… „Õ «Ã… ··œ⁄„",
                    FamilyCatgoryId = 1, // Widows category
                    HousingType = HousingType.Property,
                    BlockId = 1
                },
                new Family
                {
                    Id = 2,
                    Name = "⁄«∆·… «·”⁄Ìœ",
                    Location = "‘«—⁄ «·⁄—Ê»…° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… „ Ê”ÿ… «·œŒ·",
                    FamilyCatgoryId = 3, // Category C
                    HousingType = HousingType.Rent,
                    BlockId = 1
                },
                // Block 2 - 2 families
                new Family
                {
                    Id = 3,
                    Name = "⁄«∆·… «·ﬁÕÿ«‰Ì",
                    Location = "‘«—⁄ «·√„Ì— ”·ÿ«‰° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… Ì Ì„…",
                    FamilyCatgoryId = 2, // Orphans category
                    HousingType = HousingType.Property,
                    BlockId = 2
                },
                new Family
                {
                    Id = 4,
                    Name = "⁄«∆·… «·œÊ”—Ì",
                    Location = "‘«—⁄ «·„·ﬂ ⁄»œ«·⁄“Ì“° ÕÌ «·”·«„",
                    FamilyNotes = null,
                    FamilyCatgoryId = 3, // Category C
                    HousingType = HousingType.Rent,
                    BlockId = 2
                },
                // Block 3 - 2 families
                new Family
                {
                    Id = 5,
                    Name = "⁄«∆·… «·‘„—Ì",
                    Location = "‘«—⁄ «·Œ·ÌÃ° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… ﬂ»Ì—…",
                    FamilyCatgoryId = 1, // Widows category
                    HousingType = HousingType.Property,
                    BlockId = 3
                },
                new Family
                {
                    Id = 6,
                    Name = "⁄«∆·… «·€«„œÌ",
                    Location = "‘«—⁄ «·√„·° ÕÌ «·”·«„",
                    FamilyNotes = null,
                    FamilyCatgoryId = 3, // Category C
                    HousingType = HousingType.Rent,
                    BlockId = 3
                },
                // Block 4 - 2 families
                new Family
                {
                    Id = 7,
                    Name = "⁄«∆·… «·⁄„—Ì",
                    Location = "‘«—⁄ «·‰ŒÌ·° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… „Õ «Ã…",
                    FamilyCatgoryId = 2, // Orphans category
                    HousingType = HousingType.Property,
                    BlockId = 4
                },
                new Family
                {
                    Id = 8,
                    Name = "⁄«∆·… «·„ÿÌ—Ì",
                    Location = "‘«—⁄ «·Ê—Êœ° ÕÌ «·”·«„",
                    FamilyNotes = null,
                    FamilyCatgoryId = 3, // Category C
                    HousingType = HousingType.Rent,
                    BlockId = 4
                },
                // Block 5 - 2 families
                new Family
                {
                    Id = 9,
                    Name = "⁄«∆·… «·“Â—«‰Ì",
                    Location = "‘«—⁄ «·”·«„° ÕÌ «·”·«„",
                    FamilyNotes = "⁄«∆·… √—„·…",
                    FamilyCatgoryId = 1, // Widows category
                    HousingType = HousingType.Property,
                    BlockId = 5
                },
                new Family
                {
                    Id = 10,
                    Name = "⁄«∆·… «·Õ—»Ì",
                    Location = "‘«—⁄ «·›ÌÕ«¡° ÕÌ «·”·«„",
                    FamilyNotes = null,
                    FamilyCatgoryId = 3, // Category C
                    HousingType = HousingType.Rent,
                    BlockId = 5
                }
            );
        }
    }
}
