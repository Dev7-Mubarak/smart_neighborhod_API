using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites.Enums;

public class FamilyConfiguration : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.ToTable("Families");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.Location)
            .HasMaxLength(200);

        builder.Property(f => f.FamilyNotes)
            .HasMaxLength(500);

        // FamilyCatgory relationship owned by FamilyCatgoryConfiguration
        builder.Property(f => f.FamilyCatgoryId).IsRequired();

        builder.Property(p => p.HousingType)
            .IsRequired();

        // Block relationship owned by BlockEntityTypeConfiguration / ApplicationDbContext
        builder.Property(f => f.BlockId).IsRequired();

        builder.HasMany(f => f.FamilyMembers)
            .WithOne(fm => fm.Family)
            .HasForeignKey(fm => fm.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed: five families in حي الثوره (BlockId = 1)
        builder.HasData(
            new
            {
                Id = 1,
                Name = "عائلة الصالح",
                Location = "الشارع الأول، حي الثوره",
                FamilyNotes = (string?)null,
                FamilyCatgoryId = 1,
                HousingType = HousingType.Property,
                BlockId = 1
            },
            new
            {
                Id = 2,
                Name = "عائلة البدر",
                Location = "الشارع الثاني، حي الثوره",
                FamilyNotes = (string?)null,
                FamilyCatgoryId = 2,
                HousingType = HousingType.Rent,
                BlockId = 1
            },
            new
            {
                Id = 3,
                Name = "عائلة الحمدان",
                Location = "الشارع الثالث، حي الثوره",
                FamilyNotes = (string?)null,
                FamilyCatgoryId = 1,
                HousingType = HousingType.Property,
                BlockId = 1
            },
            new
            {
                Id = 4,
                Name = "عائلة الرشيد",
                Location = "الشارع الرابع، حي الثوره",
                FamilyNotes = (string?)null,
                FamilyCatgoryId = 3,
                HousingType = HousingType.Rent,
                BlockId = 1
            },
            new
            {
                Id = 5,
                Name = "عائلة النعيمي",
                Location = "الشارع الخامس، حي الثوره",
                FamilyNotes = (string?)null,
                FamilyCatgoryId = 2,
                HousingType = HousingType.Property,
                BlockId = 1
            }
        );
    }
}
