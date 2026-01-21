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
            // Seed manager and resident Person data (deterministic IDs)
            builder.HasData(
                // Managers (required by AppUser seeds)
                new Person
                {
                    Id = 3,
                    FirstName = "سالم",
                    SecondName = "بن راشد",
                    ThirdName = "عوض",
                    LastName = "العمري",
                    PhoneNumber = "0771122334",
                    DateOfBirth = new DateTime(1982, 4, 5),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.UnitManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 4,
                    FirstName = "خالد",
                    SecondName = "بن حمد",
                    ThirdName = "محمد",
                    LastName = "الغرمي",
                    PhoneNumber = "0772233445",
                    DateOfBirth = new DateTime(1979, 9, 12),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 5,
                    FirstName = "يوسف",
                    SecondName = "محمد",
                    ThirdName = "سالم",
                    LastName = "الملاح",
                    PhoneNumber = "0773344556",
                    DateOfBirth = new DateTime(1985, 2, 20),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ABNegative,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 6,
                    FirstName = "عبدالله",
                    SecondName = "سالم",
                    ThirdName = "محمد",
                    LastName = "الهادي",
                    PhoneNumber = "0774455667",
                    DateOfBirth = new DateTime(1980, 6, 30),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ABPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 7,
                    FirstName = "علي",
                    SecondName = "حسن",
                    ThirdName = "سالم",
                    LastName = "الدفعي",
                    PhoneNumber = "0775566778",
                    DateOfBirth = new DateTime(1986, 11, 4),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },
                new Person
                {
                    Id = 8,
                    FirstName = "ناصر",
                    SecondName = "عبد الرحمن",
                    ThirdName = "بدر",
                    LastName = "القحطاني",
                    PhoneNumber = "0776677889",
                    DateOfBirth = new DateTime(1978, 3, 17),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.BlockManager,
                    IsWhatsapp = true,
                    IsContactNumber = true
                },

                // Residents (each will be linked to exactly one Family)
                // Family 1 (Block 1)
                new Person
                {
                    Id = 11,
                    FirstName = "محمد",
                    SecondName = "سعيد",
                    ThirdName = "صالح",
                    LastName = "الغانم",
                    PhoneNumber = "0791110001",
                    DateOfBirth = new DateTime(1975, 1, 10),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen,
                    Job = "سائق"
                },
                new Person
                {
                    Id = 12,
                    FirstName = "مريم",
                    SecondName = "صالح",
                    ThirdName = "محمد",
                    LastName = "الغانم",
                    PhoneNumber = "0791110002",
                    DateOfBirth = new DateTime(1978, 5, 2),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 13,
                    FirstName = "سعيد",
                    SecondName = "محمد",
                    ThirdName = "سعيد",
                    LastName = "الغانم",
                    PhoneNumber = "0791110003",
                    DateOfBirth = new DateTime(2005, 9, 15),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 2 (Block 1)
                new Person
                {
                    Id = 14,
                    FirstName = "أحمد",
                    SecondName = "عبد الله",
                    ThirdName = "خليل",
                    LastName = "الملاح",
                    PhoneNumber = "0791122001",
                    DateOfBirth = new DateTime(1980, 7, 12),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 15,
                    FirstName = "سمية",
                    SecondName = "خليل",
                    ThirdName = "عبد الله",
                    LastName = "الملاح",
                    PhoneNumber = "0791122002",
                    DateOfBirth = new DateTime(1982, 10, 8),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ABNegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 16,
                    FirstName = "هند",
                    SecondName = "أحمد",
                    ThirdName = "خليل",
                    LastName = "الملاح",
                    PhoneNumber = "0791122003",
                    DateOfBirth = new DateTime(2008, 4, 3),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 3 (Block 2)
                new Person
                {
                    Id = 17,
                    FirstName = "عبد الولي",
                    SecondName = "بن سالم",
                    ThirdName = "خميس",
                    LastName = "العكبري",
                    PhoneNumber = "0791133001",
                    DateOfBirth = new DateTime(1970, 3, 21),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 18,
                    FirstName = "نورة",
                    SecondName = "خميس",
                    ThirdName = "بن سالم",
                    LastName = "العكبري",
                    PhoneNumber = "0791133002",
                    DateOfBirth = new DateTime(1974, 6, 11),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ANegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 19,
                    FirstName = "سارة",
                    SecondName = "عبد الولي",
                    ThirdName = "خميس",
                    LastName = "العكبري",
                    PhoneNumber = "0791133003",
                    DateOfBirth = new DateTime(2006, 12, 25),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 4 (Block 2)
                new Person
                {
                    Id = 20,
                    FirstName = "خالد",
                    SecondName = "سعيد",
                    ThirdName = "خالد",
                    LastName = "العمري",
                    PhoneNumber = "0791144001",
                    DateOfBirth = new DateTime(1983, 2, 28),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 21,
                    FirstName = "رنا",
                    SecondName = "خالد",
                    ThirdName = "سعيد",
                    LastName = "العمري",
                    PhoneNumber = "0791144002",
                    DateOfBirth = new DateTime(1985, 8, 9),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 22,
                    FirstName = "مازن",
                    SecondName = "خالد",
                    ThirdName = "سعيد",
                    LastName = "العمري",
                    PhoneNumber = "0791144003",
                    DateOfBirth = new DateTime(2010, 5, 5),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 5 (Block 3)
                new Person
                {
                    Id = 23,
                    FirstName = "سالم",
                    SecondName = "المعطي",
                    ThirdName = "بن",
                    LastName = "الهاشمي",
                    PhoneNumber = "0791155001",
                    DateOfBirth = new DateTime(1976, 10, 14),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 24,
                    FirstName = "هناء",
                    SecondName = "سالم",
                    ThirdName = "المعطي",
                    LastName = "الهاشمي",
                    PhoneNumber = "0791155002",
                    DateOfBirth = new DateTime(1979, 1, 22),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ABNegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 25,
                    FirstName = "ليلى",
                    SecondName = "سالم",
                    ThirdName = "المعطي",
                    LastName = "الهاشمي",
                    PhoneNumber = "0791155003",
                    DateOfBirth = new DateTime(2009, 9, 1),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 6 (Block 3)
                new Person
                {
                    Id = 26,
                    FirstName = "هشام",
                    SecondName = "عبد الله",
                    ThirdName = "بن",
                    LastName = "الحمادي",
                    PhoneNumber = "0791166001",
                    DateOfBirth = new DateTime(1988, 4, 20),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 27,
                    FirstName = "نهى",
                    SecondName = "هشام",
                    ThirdName = "عبد الله",
                    LastName = "الحمادي",
                    PhoneNumber = "0791166002",
                    DateOfBirth = new DateTime(1990, 12, 2),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 28,
                    FirstName = "ريان",
                    SecondName = "هشام",
                    ThirdName = "عبد الله",
                    LastName = "الحمادي",
                    PhoneNumber = "0791166003",
                    DateOfBirth = new DateTime(2012, 7, 9),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.BPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 7 (Block 4)
                new Person
                {
                    Id = 29,
                    FirstName = "عبد الكريم",
                    SecondName = "حسن",
                    ThirdName = "صالح",
                    LastName = "الحدري",
                    PhoneNumber = "0791177001",
                    DateOfBirth = new DateTime(1972, 11, 11),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 30,
                    FirstName = "فاطمة",
                    SecondName = "عبد الكريم",
                    ThirdName = "حسن",
                    LastName = "الحدري",
                    PhoneNumber = "0791177002",
                    DateOfBirth = new DateTime(1975, 2, 6),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 31,
                    FirstName = "ريان",
                    SecondName = "عبد الكريم",
                    ThirdName = "حسن",
                    LastName = "الحدري",
                    PhoneNumber = "0791177003",
                    DateOfBirth = new DateTime(2007, 3, 3),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.OPositive,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // Family 8 (Block 4)
                new Person
                {
                    Id = 32,
                    FirstName = "يوسف",
                    SecondName = "عبد الله",
                    ThirdName = "علي",
                    LastName = "البساطي",
                    PhoneNumber = "0791188001",
                    DateOfBirth = new DateTime(1986, 9, 19),
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.APositive,
                    OccupationStatus = OccupationStatus.Employee,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 33,
                    FirstName = "سعاد",
                    SecondName = "يوسف",
                    ThirdName = "عبد الله",
                    LastName = "البساطي",
                    PhoneNumber = "0791188002",
                    DateOfBirth = new DateTime(1988, 11, 23),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Married,
                    BloodType = BloodType.BNegative,
                    OccupationStatus = OccupationStatus.Unemployed,
                    personType = PersonType.Citizen
                },
                new Person
                {
                    Id = 34,
                    FirstName = "عائشة",
                    SecondName = "يوسف",
                    ThirdName = "عبد الله",
                    LastName = "البساطي",
                    PhoneNumber = "0791188003",
                    DateOfBirth = new DateTime(2011, 6, 6),
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    BloodType = BloodType.ONegative,
                    OccupationStatus = OccupationStatus.Student,
                    personType = PersonType.Citizen
                },

                // New residents for families 11-20
                new Person { Id = 35, FirstName = "سعد", SecondName = "محمد", ThirdName = "الشريف", LastName = "المرزوقي", PhoneNumber = "0791199001", DateOfBirth = new DateTime(1979, 5, 5), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 36, FirstName = "هناء", SecondName = "سعد", ThirdName = "محمد", LastName = "المرزوقي", PhoneNumber = "0791199002", DateOfBirth = new DateTime(1981, 8, 12), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 37, FirstName = "عمر", SecondName = "سعيد", ThirdName = "الزبيري", LastName = "الزبيري", PhoneNumber = "0791199003", DateOfBirth = new DateTime(1982, 3, 14), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.OPositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 38, FirstName = "نجلاء", SecondName = "عمر", ThirdName = "سعيد", LastName = "الزبيري", PhoneNumber = "0791199004", DateOfBirth = new DateTime(1984, 11, 2), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ABNegative, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 39, FirstName = "عبد الرحمن", SecondName = "محيي", ThirdName = "الهاشمي", LastName = "الهاشمي", PhoneNumber = "0791199005", DateOfBirth = new DateTime(1975, 6, 9), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 40, FirstName = "آمنة", SecondName = "عبد الرحمن", ThirdName = "محيي", LastName = "الهاشمي", PhoneNumber = "0791199006", DateOfBirth = new DateTime(1978, 2, 18), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ONegative, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 41, FirstName = "قاسم", SecondName = "علي", ThirdName = "العرادي", LastName = "العرادي", PhoneNumber = "0791199007", DateOfBirth = new DateTime(1986, 12, 1), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 42, FirstName = "سلوى", SecondName = "قاسم", ThirdName = "علي", LastName = "العرادي", PhoneNumber = "0791199008", DateOfBirth = new DateTime(1987, 4, 9), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 43, FirstName = "ماهر", SecondName = "خالد", ThirdName = "القاضي", LastName = "القاضي", PhoneNumber = "0791199009", DateOfBirth = new DateTime(1974, 7, 22), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ONegative, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },
                new Person { Id = 44, FirstName = "مريم", SecondName = "ماهر", ThirdName = "خالد", LastName = "القاضي", PhoneNumber = "0791199010", DateOfBirth = new DateTime(1976, 9, 30), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 45, FirstName = "سيف", SecondName = "عوض", ThirdName = "الربيعي", LastName = "الربيعي", PhoneNumber = "0791199011", DateOfBirth = new DateTime(1989, 1, 12), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 46, FirstName = "هاجر", SecondName = "سيف", ThirdName = "عوض", LastName = "الربيعي", PhoneNumber = "0791199012", DateOfBirth = new DateTime(1990, 5, 27), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ANegative, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },

                new Person { Id = 47, FirstName = "عبد الله", SecondName = "محمود", ThirdName = "الحاج", LastName = "الحاج", PhoneNumber = "0791199013", DateOfBirth = new DateTime(1973, 10, 3), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },
                new Person { Id = 48, FirstName = "هدى", SecondName = "عبد الله", ThirdName = "محمود", LastName = "الحاج", PhoneNumber = "0791199014", DateOfBirth = new DateTime(1975, 12, 19), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 49, FirstName = "صالح", SecondName = "علي", ThirdName = "السقاف", LastName = "السقاف", PhoneNumber = "0791199015", DateOfBirth = new DateTime(1984, 2, 28), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.OPositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 50, FirstName = "نجلاء", SecondName = "صالح", ThirdName = "علي", LastName = "السقاف", PhoneNumber = "0791199016", DateOfBirth = new DateTime(1986, 6, 6), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 51, FirstName = "هاني", SecondName = "سالم", ThirdName = "العولقي", LastName = "العولقي", PhoneNumber = "0791199017", DateOfBirth = new DateTime(1988, 11, 11), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ABPositive, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 52, FirstName = "صفية", SecondName = "هاني", ThirdName = "سالم", LastName = "العولقي", PhoneNumber = "0791199018", DateOfBirth = new DateTime(1990, 8, 8), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.BPositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen },

                new Person { Id = 53, FirstName = "مختار", SecondName = "سليم", ThirdName = "مختار", LastName = "مختار", PhoneNumber = "0791199019", DateOfBirth = new DateTime(1977, 3, 3), Gender = Gender.Male, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.ONegative, OccupationStatus = OccupationStatus.Employee, personType = PersonType.Citizen },
                new Person { Id = 54, FirstName = "أماني", SecondName = "مختار", ThirdName = "سليم", LastName = "مختار", PhoneNumber = "0791199020", DateOfBirth = new DateTime(1979, 4, 4), Gender = Gender.Female, MaritalStatus = MaritalStatus.Married, BloodType = BloodType.APositive, OccupationStatus = OccupationStatus.Unemployed, personType = PersonType.Citizen }
            );
        }
    }
}
