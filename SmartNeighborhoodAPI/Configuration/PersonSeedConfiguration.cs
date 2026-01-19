using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Configuration
{
    public class PersonSeedConfiguration 
        : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Seed Person data for managers
            builder.HasData(
                new Person
                {
                    Id = 222,
                    FirstName = "√Õ„œ",
                    SecondName = "„Õ„œ",
                    ThirdName = "⁄·Ì",
                    LastName = "«·”⁄Ìœ",
                    PhoneNumber = "0501234567",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 223,
                    FirstName = "Œ«·œ",
                    SecondName = "⁄»œ«··Â",
                    ThirdName = "„Õ„œ",
                    LastName = "«·⁄„—Ì",
                    PhoneNumber = "0502345678",
                    DateOfBirth = new DateTime(1988, 8, 20),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 224,
                    FirstName = "„Õ„œ",
                    SecondName = "”⁄Ìœ",
                    ThirdName = "√Õ„œ",
                    LastName = "«·√Õ„œÌ",
                    PhoneNumber = "0503456789",
                    DateOfBirth = new DateTime(1990, 3, 10),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 225,
                    FirstName = "⁄»œ«·—Õ„‰",
                    SecondName = "›Ì’·",
                    ThirdName = "⁄»œ«··Â",
                    LastName = "«·ﬁÕÿ«‰Ì",
                    PhoneNumber = "0504567890",
                    DateOfBirth = new DateTime(1987, 12, 5),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ABPositive,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 226,
                    FirstName = "”⁄Êœ",
                    SecondName = "ÌÊ”›",
                    ThirdName = "⁄·Ì",
                    LastName = "«·œÊ”—Ì",
                    PhoneNumber = "0505678901",
                    DateOfBirth = new DateTime(1992, 7, 18),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 227,
                    FirstName = "›Âœ",
                    SecondName = "≈»—«ÂÌ„",
                    ThirdName = "„Õ„œ",
                    LastName = "«·‘„—Ì",
                    PhoneNumber = "0506789012",
                    DateOfBirth = new DateTime(1989, 11, 25),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.UnitManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 228,
                    FirstName = "‰«’—",
                    SecondName = "⁄·Ì",
                    ThirdName = "”⁄œ",
                    LastName = "«·€«„œÌ",
                    PhoneNumber = "0507890123",
                    DateOfBirth = new DateTime(1991, 2, 14),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.UnitManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                }
            );
        }
    }
}
