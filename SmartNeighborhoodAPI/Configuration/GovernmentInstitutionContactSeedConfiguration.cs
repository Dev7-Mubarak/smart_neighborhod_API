using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class GovernmentInstitutionContactSeedConfiguration : IEntityTypeConfiguration<GovernmentInstitutionContact>
    {
        public void Configure(EntityTypeBuilder<GovernmentInstitutionContact> builder)
        {
            builder.HasData(
                new GovernmentInstitutionContact { Id = 1, GovernmentInstitutionId = 1, Name = "مهندس علي سالم", Job = "مدير محطة المياه", Phone = "0777001111" },
                new GovernmentInstitutionContact { Id = 2, GovernmentInstitutionId = 1, Name = "أمينة محمد", Job = "مسؤولة الصرف الصحي", Phone = "0777002222" },
                new GovernmentInstitutionContact { Id = 3, GovernmentInstitutionId = 2, Name = "نقيب خالد الشامي", Job = "ضابط ارتباط", Phone = "0777013333" }
            );
        }
    }
}
