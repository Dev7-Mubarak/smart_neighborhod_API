namespace OurProjectSmartNeiborhood.Configuration
{
    public class FamilyCatgoryConfiguration :IEntityTypeConfiguration<FamilyCatgory>
    {
        public void Configure(EntityTypeBuilder<FamilyCatgory> builder)
        {
            builder.HasKey(fc => fc.Id);

            builder.Property(fc => fc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(fc => fc.Families)
                   .WithOne(f => f.FamilyCatgory) 
                   .HasForeignKey(f => f.FamilyCatgoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                    new FamilyCatgory { Id = 1, Name = "A" },
                    new FamilyCatgory { Id = 2, Name = "B" },
                    new FamilyCatgory { Id = 3, Name = "C" }
                );
        }
    }
}
