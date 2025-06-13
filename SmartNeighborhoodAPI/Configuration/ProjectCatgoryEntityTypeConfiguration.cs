namespace OurProjectSmartNeiborhood.Configuration
{
    public class ProjectCatgoryEntityTypeConfiguration:IEntityTypeConfiguration<ProjectCatogory>
    {
        public void Configure(EntityTypeBuilder<ProjectCatogory> builder)
        {
            builder
                .Property(d => d.Description)
                .HasMaxLength(120);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder
                .HasIndex(d => d.Name)
                .IsUnique();

            // Seed Arabic data
            builder.HasData(
                new ProjectCatogory
                {
                    Id = 1,
                    Name = "سكني",
                    Description = "مشاريع سكنية"
                },
                new ProjectCatogory
                {
                    Id = 2,
                    Name = "تجاري",
                    Description = "مشاريع تجارية"
                },
                new ProjectCatogory
                {
                    Id = 3,
                    Name = "تعليمي",
                    Description = "مشاريع تعليمية"
                },
                new ProjectCatogory
                {
                    Id = 4,
                    Name = "مساعدات",
                    Description = "مشاريع مساعدات"
                }
            );
        }
    }
}
