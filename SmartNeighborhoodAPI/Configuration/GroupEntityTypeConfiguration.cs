using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(g => g.Ads)
                .WithOne()
                .HasForeignKey("GroupId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Group { Id = 1, Name = "مجموعة الحي العام" },
                new Group { Id = 2, Name = "مجموعة الشباب" }
            );
        }    }
}