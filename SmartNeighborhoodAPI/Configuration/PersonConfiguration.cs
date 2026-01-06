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
                    Id = 9,
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
                },
                new Person
                {
                    Id = 10,
                    FirstName = "فاطمة",
                    SecondName = "خالد",
                    ThirdName = "عبد الله",
                    LastName = "الحسني",
                    PhoneNumber = "0789876543",
                    Gender = Gender.Female,
                    BloodType = BloodType.ABNegative,
                    OccupationStatus = OccupationStatus.Student,
                    MaritalStatus = MaritalStatus.Single,
                    personType = PersonType.Citizen,
                    Job = "طالبة"
                },
                new Person
                {
                    Id = 11,
                    FirstName = "محمد",
                    SecondName = "علي",
                    ThirdName = "يوسف",
                    LastName = "الكعبي",
                    PhoneNumber = "0771122334",
                    Gender = Gender.Male,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    MaritalStatus = MaritalStatus.Married,
                    personType = PersonType.Citizen,
                    Job = "محاسب"
                },
                new Person
                {
                    Id = 12,
                    FirstName = "ريم",
                    SecondName = "حسن",
                    ThirdName = "سالم",
                    LastName = "الشامسي",
                    PhoneNumber = "0799988776",
                    Gender = Gender.Female,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.HouseWife,
                    MaritalStatus = MaritalStatus.Married,
                    personType = PersonType.Citizen,
                    Job = "ربة منزل"
                },
                new Person
                {
                    Id = 13,
                    FirstName = "سالم",
                    SecondName = "ناصر",
                    ThirdName = "محمد",
                    LastName = "المري",
                    PhoneNumber = "0785544332",
                    Gender = Gender.Male,
                    BloodType = BloodType.BNegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    MaritalStatus = MaritalStatus.Single,
                    personType = PersonType.Citizen,
                    Job = "عاطل عن العمل"
                },
                new Person
                {
                    Id = 14,
                    FirstName = "نورة",
                    SecondName = "عبدالرحمن",
                    ThirdName = "خليل",
                    LastName = "العنزي",
                    PhoneNumber = "0777766554",
                    Gender = Gender.Female,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Student,
                    MaritalStatus = MaritalStatus.Single,
                    personType = PersonType.Citizen,
                    Job = "طالبة جامعية"
                },
                new Person
                {
                    Id = 15,
                    FirstName = "خالد",
                    SecondName = "ابراهيم",
                    ThirdName = "مصطفى",
                    LastName = "الهاشمي",
                    PhoneNumber = "0796655443",
                    Gender = Gender.Male,
                    BloodType = BloodType.ABPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    MaritalStatus = MaritalStatus.Divorced,
                    personType = PersonType.Citizen,
                    Job = "مدير مبيعات"
                },
                new Person
                {
                    Id = 16,
                    FirstName = "زينب",
                    SecondName = "محمود",
                    ThirdName = "فارس",
                    LastName = "الخطيب",
                    PhoneNumber = "0782233445",
                    Gender = Gender.Female,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    MaritalStatus = MaritalStatus.Widowed,
                    personType = PersonType.Citizen,
                    Job = "مدرسة"
                },
                new Person
                {
                    Id = 17,
                    FirstName = "عمر",
                    SecondName = "فهد",
                    ThirdName = "جابر",
                    LastName = "السويدي",
                    PhoneNumber = "0773344556",
                    Gender = Gender.Male,
                    BloodType = BloodType.ANegative,
                    OccupationStatus = OccupationStatus.SelfEmployed,
                    MaritalStatus = MaritalStatus.Married,
                    personType = PersonType.Citizen,
                    Job = "تاجر"
                },
                new Person
                {
                    Id = 18,
                    FirstName = "منى",
                    SecondName = "ياسر",
                    ThirdName = "سلطان",
                    LastName = "المنصوري",
                    PhoneNumber = "0794455667",
                    Gender = Gender.Female,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Student,
                    MaritalStatus = MaritalStatus.Single,
                    personType = PersonType.Citizen,
                    Job = "طالبة مدرسة"
                }
            );
        }
    }
}
