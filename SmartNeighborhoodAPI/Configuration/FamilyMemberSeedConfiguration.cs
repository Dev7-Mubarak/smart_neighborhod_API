using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class FamilyMemberSeedConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.HasData(
                // ── عائلة الصالح (FamilyId = 1) ──
                // 1 - أحمد الصالح → أب
                new FamilyMember { Id = 1, PersonId = 5, FamilyId = 1, MemberFamilyRoleId = 1 },
                // 2 - فاطمة الصالح → أم
                new FamilyMember { Id = 2, PersonId = 6, FamilyId = 1, MemberFamilyRoleId = 2 },
                // 3 - محمد الصالح → ابن
                new FamilyMember { Id = 3, PersonId = 7, FamilyId = 1, MemberFamilyRoleId = 3 },
                // 4 - سارة الصالح → ابنة
                new FamilyMember { Id = 4, PersonId = 8, FamilyId = 1, MemberFamilyRoleId = 4 },

                // ── عائلة البدر (FamilyId = 2) ──
                // 5 - عمر البدر → أب
                new FamilyMember { Id = 5, PersonId = 9, FamilyId = 2, MemberFamilyRoleId = 1 },
                // 6 - سلمى البدر → أم
                new FamilyMember { Id = 6, PersonId = 10, FamilyId = 2, MemberFamilyRoleId = 2 },
                // 7 - يوسف البدر → ابن
                new FamilyMember { Id = 7, PersonId = 11, FamilyId = 2, MemberFamilyRoleId = 3 },

                // ── عائلة الحمدان (FamilyId = 3) ──
                new FamilyMember { Id = 8, PersonId = 12, FamilyId = 3, MemberFamilyRoleId = 1 }, // أب
                new FamilyMember { Id = 9, PersonId = 13, FamilyId = 3, MemberFamilyRoleId = 2 }, // أم
                new FamilyMember { Id = 10, PersonId = 14, FamilyId = 3, MemberFamilyRoleId = 3 }, // ابن
                new FamilyMember { Id = 11, PersonId = 15, FamilyId = 3, MemberFamilyRoleId = 4 }, // ابنة

                // ── عائلة الرشيد (FamilyId = 4) ──
                new FamilyMember { Id = 12, PersonId = 16, FamilyId = 4, MemberFamilyRoleId = 1 }, // أب
                new FamilyMember { Id = 13, PersonId = 17, FamilyId = 4, MemberFamilyRoleId = 2 }, // أم
                new FamilyMember { Id = 14, PersonId = 18, FamilyId = 4, MemberFamilyRoleId = 3 }, // ابن

                // ── عائلة النعيمي (FamilyId = 5) ──
                new FamilyMember { Id = 15, PersonId = 19, FamilyId = 5, MemberFamilyRoleId = 1 }, // أب
                new FamilyMember { Id = 16, PersonId = 20, FamilyId = 5, MemberFamilyRoleId = 2 }  // أم
            );
        }
    }
}
