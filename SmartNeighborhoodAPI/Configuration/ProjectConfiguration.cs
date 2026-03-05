using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(p => p.Description)
                .HasMaxLength(200);

            builder.Property(p => p.ProjectCatogoryId)
                .IsRequired();

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.EndDate)
                .IsRequired(false);

            builder.Property(p => p.ProjectStatus)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.ProjectPriority)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.Budget)
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            // ManagerId is int? but AppUser.Id is string — types are incompatible.
            // Ignore the nav to prevent EF from trying to build an invalid FK relationship.
            builder.Ignore(p => p.Manager);
            builder.Property(p => p.ManagerId).IsRequired(false);

            builder.HasOne(p => p.ProjectCatogory)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ProjectCatogoryId)
                .OnDelete(DeleteBehavior.Cascade);


            // 🌱 Seed three projects (assumes ProjectCatogoryIds already exist)
            builder.HasData(
                new Project
                {
                    Id = 1,
                    Name = "مشروع تجريبي",
                    Description = "هذا مشروع تمهيدي",
                    ProjectCatogoryId = 1,
                    StartDate = new DateTime(2025, 1, 1),
                    EndDate = new DateTime(2025, 12, 31),
                    ProjectStatus = ProjectStatus.Planned,
                    ProjectPriority = ProjectPriority.Medium,
                    Budget = 100000
                },
                new Project
                {
                    Id = 2,
                    Name = "حملة نظافة الحي",
                    Description = "تحسين النظافة والمرافق في المنطقة",
                    ProjectCatogoryId = 4,
                    StartDate = new DateTime(2025, 3, 1),
                    EndDate = new DateTime(2025, 3, 31),
                    ProjectStatus = ProjectStatus.Planned,
                    ProjectPriority = ProjectPriority.High,
                    Budget = 15000
                },
                new Project
                {
                    Id = 3,
                    Name = "تطوير ملعب الحي",
                    Description = "إنشاء ملعب مصغر وتجهيزات رياضية",
                    ProjectCatogoryId = 1,
                    StartDate = new DateTime(2025, 4, 15),
                    EndDate = new DateTime(2025, 9, 15),
                    ProjectStatus = ProjectStatus.Planned,
                    ProjectPriority = ProjectPriority.Medium,
                    Budget = 40000
                }
            );
        }
    }
}
