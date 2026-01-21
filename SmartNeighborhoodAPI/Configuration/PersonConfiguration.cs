using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites.Enums;

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
                .HasMaxLength(20);

            builder.Property(p => p.DateOfBirth)
            .HasColumnType("date")
            .IsRequired(false);

            builder.Property(p => p.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(30);

            builder.Property(p => p.Gender)
                .HasMaxLength(10);

            builder.Property(p => p.BloodType)
                .IsRequired();


            builder.Property(p => p.OccupationStatus)
                .IsRequired();

            builder.Property(p => p.MaritalStatus)
                .IsRequired();

            builder.Property(p => p.personType)
                 .IsRequired();

            builder.Property(p => p.Job)
                .HasMaxLength(30);

            builder.HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "أحمد",
                    SecondName = "سعيد",
                    ThirdName = "محمود",
                    LastName = "الزهيري",
                    PhoneNumber = "0791234567",
                    Gender = Gender.Male,
                    BloodType = BloodType.ABNegative,
                    OccupationStatus = OccupationStatus.Employee,
                    MaritalStatus = MaritalStatus.Married,
                    personType = PersonType.Admin,
                    Job = "مهندس"
                }
     
            );
        }
    }
}
