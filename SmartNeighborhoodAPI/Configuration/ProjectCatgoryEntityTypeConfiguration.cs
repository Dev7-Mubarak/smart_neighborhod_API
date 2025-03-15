namespace OurProjectSmartNeiborhood.Configuration
{
    public class ProjectCatgoryEntityTypeConfiguration:IEntityTypeConfiguration<ProjectCatogory>
    {
        public void Configure(EntityTypeBuilder<ProjectCatogory> builder)
        {
            builder
               .Property(D => D.Description)
               .HasMaxLength(120);
        }
    }
}
