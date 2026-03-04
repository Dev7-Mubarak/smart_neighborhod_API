using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class TeamSeedConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Team { Id = 1, Name = "فريق تطوير الحي" },
                new Team { Id = 2, Name = "فريق حملة النظافة" }
            );
        }
    }
}
