//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using SmartNeighborhoodAPI.Entites;

//namespace SmartNeighborhoodAPI.Configuration
//{
//    public class ProjectTeamSeedConfiguration : IEntityTypeConfiguration<ProjectTeam>
//    {
//        public void Configure(EntityTypeBuilder<ProjectTeam> builder)
//        {
//            builder.HasData(
//                // Link existing Project Id = 1 to Team Id = 1
//                new ProjectTeam { Id = 1, ProjectId = 1, TeamId = 1 },
//                new ProjectTeam { Id = 2, ProjectId = 2, TeamId = 1 },
//                new ProjectTeam { Id = 3, ProjectId = 3, TeamId = 2 }
//            );
//        }
//    }
//}
