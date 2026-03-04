using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    /// <summary>
    /// TeamRoles: 1=مدير المشروع  2=النائب  3=عضو
    /// Teams: 1=فريق تطوير الحي   2=فريق حملة النظافة
    /// Persons used: 5,6,9,12,16,19
    /// </summary>
    public class TeamMemberSeedConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasData(
                // ── فريق تطوير الحي (Team 1) ──
                new TeamMember { Id = 1, TeamId = 1, PersonId = 5, TeamRoleId = 1, DateOfJoin = new DateTime(2025, 1, 1) }, // أحمد الصالح — مدير
                new TeamMember { Id = 2, TeamId = 1, PersonId = 9, TeamRoleId = 2, DateOfJoin = new DateTime(2025, 1, 1) }, // عمر البدر — نائب
                new TeamMember { Id = 3, TeamId = 1, PersonId = 12, TeamRoleId = 3, DateOfJoin = new DateTime(2025, 1, 15) }, // حسن الحمدان — عضو
                new TeamMember { Id = 4, TeamId = 1, PersonId = 16, TeamRoleId = 3, DateOfJoin = new DateTime(2025, 1, 15) }, // إبراهيم الرشيد — عضو

                // ── فريق حملة النظافة (Team 2) ──
                new TeamMember { Id = 5, TeamId = 2, PersonId = 19, TeamRoleId = 1, DateOfJoin = new DateTime(2025, 2, 1) }, // سلطان النعيمي — مدير
                new TeamMember { Id = 6, TeamId = 2, PersonId = 6, TeamRoleId = 2, DateOfJoin = new DateTime(2025, 2, 1) }  // فاطمة الصالح — نائب
            );
        }
    }
}
