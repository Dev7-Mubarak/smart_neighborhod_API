using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.HasOne(f => f.FamilyCatgory)
            .WithMany()
            .HasForeignKey(f => f.FamilyCatgoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.HousingType)
            .IsRequired();

        builder.HasOne(f => f.Block)
            .WithMany()
            .HasForeignKey(f => f.BlockId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.FamilyMembers)
            .WithOne()
            .HasForeignKey("FamilyId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
