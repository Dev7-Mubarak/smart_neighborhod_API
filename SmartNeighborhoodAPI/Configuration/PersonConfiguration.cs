namespace OurProjectSmartNeiborhood.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.SecondName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.ThirdName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.Gender)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.BloodType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.IdentityNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.IdentityType)
                .HasConversion<string>()
                .IsRequired();  

            builder.Property(p => p.OccupationStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.MaritalStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Job)
                .HasMaxLength(30);

            // Relationships
            builder.HasMany(p => p.FamilyMembers)
                .WithOne()
                .HasForeignKey(f => f.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
