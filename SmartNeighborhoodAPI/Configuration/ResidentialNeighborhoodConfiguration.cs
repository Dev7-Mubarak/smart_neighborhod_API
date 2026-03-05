using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ResidentialNeighborhoodConfiguration
        : IEntityTypeConfiguration<ResidentialNeighborhood>
    {
        public void Configure(EntityTypeBuilder<ResidentialNeighborhood> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(n => n.Name)
                .IsUnique();

            builder.HasMany(n => n.ResidentialUnits)
                .WithOne(u => u.ResidentialNeighborhood)
                .HasForeignKey(u => u.ResidentialNeighborhoodId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed: الحي الأول — parent of حي الثوره block
            builder.HasData(new
            {
                Id = 1,
                Name = "حي الثوره",
                NeighborhoodManagerId = "dddddddd-dddd-dddd-dddd-dddddddddddd" // RN Manager
            });
        }
    }
}
