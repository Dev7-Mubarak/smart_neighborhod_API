using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace OurProjectSmartNeiborhood.Configuration
{
    public class AdEntityTypeConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AdsText)
                .IsRequired()
                .HasMaxLength(500);

            // Ad.Groups is a wrongly typed nav property (List<Group> instead of Group).
            // Ignore it; the Group→Ads relationship is owned by GroupEntityTypeConfiguration.
            builder.Ignore(a => a.Groups);

            builder.HasData(
                new Ad { Id = 1, GroupId = 1, AdsText = "إعلان: اجتماع عام لسكان حي الثوره يوم الجمعة القادم الساعة السابعة مساءً.", CreatedTime = new DateTime(2025, 2, 1) },
                new Ad { Id = 2, GroupId = 1, AdsText = "تذكير: الرجاء الحفاظ على نظافة الشوارع والمرافق العامة في الحي.", CreatedTime = new DateTime(2025, 2, 10) },
                new Ad { Id = 3, GroupId = 2, AdsText = "دعوة للشباب للمشاركة في مبادرة تطوير ملعب الحي — التسجيل مفتوح.", CreatedTime = new DateTime(2025, 3, 5) }
            );
        }
    }
}

