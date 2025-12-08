using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

public class ResidentialUnitConfiguration : IEntityTypeConfiguration<ResidentialUnit>
{
    public void Configure(EntityTypeBuilder<ResidentialUnit> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(200);
        builder.Property(u => u.UnitManagerId).IsRequired();

        builder.HasMany(u => u.Blocks).WithOne(b => b.ResidentialUnit).HasForeignKey(b => b.ResidentialUnitId);
    }
}
