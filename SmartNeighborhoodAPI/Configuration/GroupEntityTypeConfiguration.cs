namespace OurProjectSmartNeiborhood.Configuration
{
    public class GroupEntityTypeConfiguration:IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .HasMany(A => A.Ads)
                .WithOne();

         
        }
    }
}
