using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class ConfilctTypeConfiguration : IEntityTypeConfiguration<ConfilctType>
    {
        public void Configure(EntityTypeBuilder<ConfilctType> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(ct => ct.Conflicts)
                   .WithOne(c => c.ConflictType)
                   .HasForeignKey(c => c.ConflictTypeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new ConfilctType { Id = 1, Name = "صلح" },
                new ConfilctType { Id = 2, Name = "معاهدات" },
                new ConfilctType { Id = 3, Name = "اتفاقيات" }
            );
        }
    }
}
