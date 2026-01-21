using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class GovernmentInstitutionSeedConfiguration : IEntityTypeConfiguration<GovernmentInstitution>
    {
        public void Configure(EntityTypeBuilder<GovernmentInstitution> builder)
        {
            builder.HasData(
                new GovernmentInstitution { Id = 1, Name = "مؤسسة المياه والصرف" },
                new GovernmentInstitution { Id = 2, Name = "مركز شرطة المكلا" }
            );
        }
    }
}
