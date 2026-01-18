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

            builder.Property(p => p.ManagerId)
                .IsRequired(false);

            builder.HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ProjectCatogory)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ProjectCatogoryId)
                .OnDelete(DeleteBehavior.Cascade);


            // 🌱 Seed one project (assumes ManagerId = 1 and ProjectCatogoryId = 1 already exist)
            builder.HasData(new Project
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
            });
        }
    }
}
