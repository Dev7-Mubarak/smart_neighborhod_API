using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Configuration
{
    public class PersonSeedConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(
                // ── System users (PersonId 1-4, referenced by AppUserSeedConfiguration) ──

                // 1 - Admin
                new Person
                {
                    Id = 1,
                    FirstName = "مبارك",
                    SecondName = "محمد",
                    ThirdName = "علي",
                    LastName = "الأمين",
                    PhoneNumber = "0900000001",
                    Gender = Gender.Male,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Admin,
                    ResidencyStatus = ResidencyStatus.Resident
                },

                // 2 - Block Manager
                new Person
                {
                    Id = 2,
                    FirstName = "خالد",
                    SecondName = "عمر",
                    ThirdName = "سالم",
                    LastName = "العتيبي",
                    PhoneNumber = "0900000002",
                    Gender = Gender.Male,
                    BloodType = BloodType.APositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    ResidencyStatus = ResidencyStatus.Resident
                },

                // 3 - Unit Manager
                new Person
                {
                    Id = 3,
                    FirstName = "عبدالله",
                    SecondName = "سعيد",
                    ThirdName = "حسن",
                    LastName = "المرواني",
                    PhoneNumber = "0900000003",
                    Gender = Gender.Male,
                    BloodType = BloodType.BPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.UnitManager,
                    ResidencyStatus = ResidencyStatus.Resident
                },

                // 4 - Residential Neighborhood Manager
                new Person
                {
                    Id = 4,
                    FirstName = "يوسف",
                    SecondName = "إبراهيم",
                    ThirdName = "ناصر",
                    LastName = "الزيدي",
                    PhoneNumber = "0900000004",
                    Gender = Gender.Male,
                    BloodType = BloodType.ABPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.UnitManager,
                    ResidencyStatus = ResidencyStatus.Resident
                },

                // ── Residents of حي الثوره ──

                // 5 - Family 1: عائلة الصالح — أب
                new Person
                {
                    Id = 5,
                    FirstName = "أحمد",
                    SecondName = "محمد",
                    ThirdName = "صالح",
                    LastName = "الصالح",
                    PhoneNumber = "0911100001",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Male,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "مهندس",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 6 - Family 1: عائلة الصالح — أم
                new Person
                {
                    Id = 6,
                    FirstName = "فاطمة",
                    SecondName = "علي",
                    ThirdName = "حسن",
                    LastName = "الصالح",
                    PhoneNumber = "0911100002",
                    IsWhatsapp = true,
                    IsContactNumber = false,
                    Gender = Gender.Female,
                    BloodType = BloodType.APositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "معلمة",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 7 - Family 1: عائلة الصالح — ابن
                new Person
                {
                    Id = 7,
                    FirstName = "محمد",
                    SecondName = "أحمد",
                    ThirdName = "محمد",
                    LastName = "الصالح",
                    Gender = Gender.Male,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 8 - Family 1: عائلة الصالح — ابنة
                new Person
                {
                    Id = 8,
                    FirstName = "سارة",
                    SecondName = "أحمد",
                    ThirdName = "محمد",
                    LastName = "الصالح",
                    Gender = Gender.Female,
                    BloodType = BloodType.APositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 9 - Family 2: عائلة البدر — أب
                new Person
                {
                    Id = 9,
                    FirstName = "عمر",
                    SecondName = "يوسف",
                    ThirdName = "ناصر",
                    LastName = "البدر",
                    PhoneNumber = "0922200001",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Male,
                    BloodType = BloodType.BPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "تاجر",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 10 - Family 2: عائلة البدر — أم
                new Person
                {
                    Id = 10,
                    FirstName = "سلمى",
                    SecondName = "خالد",
                    ThirdName = "نور",
                    LastName = "البدر",
                    PhoneNumber = "0922200002",
                    IsWhatsapp = false,
                    IsContactNumber = true,
                    Gender = Gender.Female,
                    BloodType = BloodType.ABPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = true,
                    ChronicDiseasesNotes = "ضغط الدم"
                },

                // 11 - Family 2: عائلة البدر — ابن
                new Person
                {
                    Id = 11,
                    FirstName = "يوسف",
                    SecondName = "عمر",
                    ThirdName = "يوسف",
                    LastName = "البدر",
                    Gender = Gender.Male,
                    BloodType = BloodType.BPositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // ── عائلة الحمدان (Family 3) ──

                // 12 - أب
                new Person
                {
                    Id = 12,
                    FirstName = "حسن",
                    SecondName = "علي",
                    ThirdName = "ناصر",
                    LastName = "الحمدان",
                    PhoneNumber = "0933300001",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Male,
                    BloodType = BloodType.APositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "محاسب",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 13 - أم
                new Person
                {
                    Id = 13,
                    FirstName = "نورة",
                    SecondName = "محمد",
                    ThirdName = "سعيد",
                    LastName = "الحمدان",
                    PhoneNumber = "0933300002",
                    IsWhatsapp = true,
                    IsContactNumber = false,
                    Gender = Gender.Female,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 14 - ابن
                new Person
                {
                    Id = 14,
                    FirstName = "عبدالرحمن",
                    SecondName = "حسن",
                    ThirdName = "علي",
                    LastName = "الحمدان",
                    Gender = Gender.Male,
                    BloodType = BloodType.APositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 15 - ابنة
                new Person
                {
                    Id = 15,
                    FirstName = "ليلى",
                    SecondName = "حسن",
                    ThirdName = "علي",
                    LastName = "الحمدان",
                    Gender = Gender.Female,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // ── عائلة الرشيد (Family 4) ──

                // 16 - أب
                new Person
                {
                    Id = 16,
                    FirstName = "إبراهيم",
                    SecondName = "عبدالله",
                    ThirdName = "فهد",
                    LastName = "الرشيد",
                    PhoneNumber = "0944400001",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Male,
                    BloodType = BloodType.BPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "مقاول",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = true,
                    ChronicDiseasesNotes = "سكري"
                },

                // 17 - أم
                new Person
                {
                    Id = 17,
                    FirstName = "منى",
                    SecondName = "يوسف",
                    ThirdName = "كريم",
                    LastName = "الرشيد",
                    PhoneNumber = "0944400002",
                    IsWhatsapp = false,
                    IsContactNumber = true,
                    Gender = Gender.Female,
                    BloodType = BloodType.ANegative,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 18 - ابن
                new Person
                {
                    Id = 18,
                    FirstName = "طارق",
                    SecondName = "إبراهيم",
                    ThirdName = "عبدالله",
                    LastName = "الرشيد",
                    Gender = Gender.Male,
                    BloodType = BloodType.BPositive,
                    MaritalStatus = MaritalStatus.Single,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // ── عائلة النعيمي (Family 5) ──

                // 19 - أب
                new Person
                {
                    Id = 19,
                    FirstName = "سلطان",
                    SecondName = "ناصر",
                    ThirdName = "راشد",
                    LastName = "النعيمي",
                    PhoneNumber = "0955500001",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Male,
                    BloodType = BloodType.OPositive,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "معلم",
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                },

                // 20 - أم
                new Person
                {
                    Id = 20,
                    FirstName = "هيفاء",
                    SecondName = "سالم",
                    ThirdName = "حمد",
                    LastName = "النعيمي",
                    PhoneNumber = "0955500002",
                    IsWhatsapp = true,
                    IsContactNumber = true,
                    Gender = Gender.Female,
                    BloodType = BloodType.ABNegative,
                    MaritalStatus = MaritalStatus.Married,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen,
                    ResidencyStatus = ResidencyStatus.Resident,
                    HasChronicDiseases = false
                }
            );
        }
    }
}
