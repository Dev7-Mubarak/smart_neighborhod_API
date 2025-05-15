namespace OurProjectSmartNeiborhood.Configuration
{
    public class FamilyTypeConfiguration:IEntityTypeConfiguration<FamilyType>
    {
        public void Configure(EntityTypeBuilder<FamilyType> builder)
        {
            builder.HasKey(ft => ft.Id);

            builder.Property(ft => ft.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(ft => ft.Families)
                   .WithOne(f => f.FamilyType)
                   .HasForeignKey(f => f.FamilyTypeId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new FamilyType { Id = 1, Name = "عائلة أيتام" },
                new FamilyType { Id = 2, Name = "عائلة أرامل" },
                new FamilyType { Id = 3, Name = "عائلة طبيعية" }
            );
        }
    }
}
