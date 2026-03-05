using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class GovernmentInstitutionConfiguration : IEntityTypeConfiguration<GovernmentInstitution>
    {
        public void Configure(EntityTypeBuilder<GovernmentInstitution> builder)
        {
            builder.HasKey(gi => gi.Id);

            builder.Property(gi => gi.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasMany(gi => gi.GovernmentInstitutionContacts)
                .WithOne(c => c.GovernmentInstitution)
                .HasForeignKey(c => c.GovernmentInstitutionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed: institutions referenced by GovernmentInstitutionContactSeedConfiguration
            builder.HasData(
                new GovernmentInstitution { Id = 1, Name = "محطة المياه والصرف الصحي" },
                new GovernmentInstitution { Id = 2, Name = "مديرية الأمن" }
            );
        }
    }
}
