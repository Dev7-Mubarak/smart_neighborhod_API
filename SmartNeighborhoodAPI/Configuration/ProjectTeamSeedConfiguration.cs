using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ProjectTeamSeedConfiguration : IEntityTypeConfiguration<ProjectTeam>
    {
        public void Configure(EntityTypeBuilder<ProjectTeam> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.HasData(
                // Project 2: حملة نظافة الحي → Team 1: فريق تطوير الحي
                new ProjectTeam { Id = 1, ProjectId = 2, TeamId = 1 },
                // Project 3: تطوير ملعب الحي → Team 2: فريق حملة النظافة
                new ProjectTeam { Id = 2, ProjectId = 3, TeamId = 2 }
            );
        }
    }
}
