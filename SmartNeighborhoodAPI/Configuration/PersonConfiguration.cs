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
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.DateOfBirth)
            .HasColumnType("date")
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
                .IsRequired();

            builder.Property(p => p.IdentityNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.IdentityType)
                .IsRequired();

            builder.Property(p => p.Gender)
                .IsRequired();


            builder.Property(p => p.OccupationStatus)
                .IsRequired();

            builder.Property(p => p.MaritalStatus)
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
                    Email = "ahmad@example.com",
                    Gender = Gender.Male,
                    BloodType = BloodType.ABNegative,
                    IdentityNumber = "894754369053",
                    IdentityType = IdentityType.IdentityCard,
                    OccupationStatus = OccupationStatus.Employee,
                    MaritalStatus = MaritalStatus.Married,
                    Job = "مهندس"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "فاطمة",
                    SecondName = "خالد",
                    ThirdName = "عبد الله",
                    LastName = "الحسني",
                    PhoneNumber = "0789876543",
                    Email = "fatima@example.com",
                    Gender = Gender.Female,
                    BloodType = BloodType.ABNegative,
                    IdentityNumber = "8945784903588",
                    IdentityType = IdentityType.IdentityCard,
                    OccupationStatus = OccupationStatus.Student,
                    MaritalStatus = MaritalStatus.Single,
                    Job = "طالبة"
                }
            );
        }
    }
}
