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

            builder.Property(p => p.Job)
                .HasMaxLength(30);

            builder.HasData(
             new Person { Id = 1, FirstName = "أحمد", SecondName = "سعيد", ThirdName = "محمود", LastName = "الزهيري", PhoneNumber = "0791234567", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1990, 5, 12), Gender = Gender.Male, BloodType = BloodType.ABNegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "مهندس", Image = null },

             new Person { Id = 2, FirstName = "فاطمة", SecondName = "خالد", ThirdName = "عبد الله", LastName = "الحسني", PhoneNumber = "0789876543", IsWhatsapp = true, IsContactNumber = false, DateOfBirth = new DateTime(1998, 3, 21), Gender = Gender.Female, BloodType = BloodType.APositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالبة", Image = null },

             new Person { Id = 3, FirstName = "محمد", SecondName = "علي", ThirdName = "سليم", LastName = "القيسي", PhoneNumber = "0795551122", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1985, 1, 1), Gender = Gender.Male, BloodType = BloodType.ONegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "محاسب", Image = null },

             new Person { Id = 4, FirstName = "مريم", SecondName = "إبراهيم", ThirdName = "حسن", LastName = "العنزي", PhoneNumber = "0784443322", IsWhatsapp = false, IsContactNumber = true, DateOfBirth = new DateTime(1995, 7, 18), Gender = Gender.Female, BloodType = BloodType.BPositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Unemployed, Job = null, Image = null },

             new Person { Id = 5, FirstName = "يوسف", SecondName = "صالح", ThirdName = "عبد الرحمن", LastName = "العجارمة", PhoneNumber = "0797778899", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(2000, 11, 3), Gender = Gender.Male, BloodType = BloodType.APositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالب", Image = null },

             new Person { Id = 6, FirstName = "ليلى", SecondName = "موسى", ThirdName = "جميل", LastName = "الطراونة", PhoneNumber = "0781122334", IsWhatsapp = true, IsContactNumber = false, DateOfBirth = new DateTime(1992, 9, 14), Gender = Gender.Female, BloodType = BloodType.ABNegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Unemployed, Job = "ربة منزل", Image = null },

             new Person { Id = 7, FirstName = "سلمان", SecondName = "زيد", ThirdName = "أحمد", LastName = "العوفي", PhoneNumber = "0793322110", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1988, 6, 28), Gender = Gender.Male, BloodType = BloodType.BNegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "فني كهرباء", Image = null },

             new Person { Id = 8, FirstName = "نهى", SecondName = "راشد", ThirdName = "سامي", LastName = "الخطيب", PhoneNumber = "0786677889", IsWhatsapp = false, IsContactNumber = true, DateOfBirth = new DateTime(1999, 12, 30), Gender = Gender.Female, BloodType = BloodType.ABNegative, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالبة", Image = null },

             new Person { Id = 9, FirstName = "حسن", SecondName = "محمود", ThirdName = "فوزي", LastName = "البقاعي", PhoneNumber = "0799988776", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1983, 2, 14), Gender = Gender.Male, BloodType = BloodType.OPositive, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "سائق", Image = null },

             new Person { Id = 10, FirstName = "نور", SecondName = "جميل", ThirdName = "عمر", LastName = "الغزاوي", PhoneNumber = "0789988223", IsWhatsapp = true, IsContactNumber = false, DateOfBirth = new DateTime(2001, 4, 9), Gender = Gender.Female, BloodType = BloodType.APositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالبة", Image = null },

             new Person { Id = 11, FirstName = "خالد", SecondName = "فهد", ThirdName = "سالم", LastName = "المعايطه", PhoneNumber = "0792211334", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1990, 10, 22), Gender = Gender.Male, BloodType = BloodType.BPositive, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "معلم", Image = null },

             new Person { Id = 12, FirstName = "سارة", SecondName = "منصور", ThirdName = "أمين", LastName = "الشروف", PhoneNumber = "0785544332", IsWhatsapp = false, IsContactNumber = true, DateOfBirth = new DateTime(1997, 8, 5), Gender = Gender.Female, BloodType = BloodType.ABNegative, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Employee, Job = "مصممة", Image = null },

             new Person { Id = 13, FirstName = "رائد", SecondName = "خليل", ThirdName = "يوسف", LastName = "عواد", PhoneNumber = "0798877665", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1986, 3, 19), Gender = Gender.Male, BloodType = BloodType.ABPositive, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "ممرض", Image = null },

             new Person { Id = 14, FirstName = "هناء", SecondName = "زيد", ThirdName = "عبدالله", LastName = "السالم", PhoneNumber = "0784433221", IsWhatsapp = true, IsContactNumber = false, DateOfBirth = new DateTime(1993, 1, 27), Gender = Gender.Female, BloodType = BloodType.BNegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Unemployed, Job = "ربة منزل", Image = null },

             new Person { Id = 15, FirstName = "عبدالله", SecondName = "محمد", ThirdName = "نادر", LastName = "العلاونة", PhoneNumber = "0791122556", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1982, 11, 11), Gender = Gender.Male, BloodType = BloodType.OPositive, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "تاجر", Image = null },

             new Person { Id = 16, FirstName = "دلال", SecondName = "سالم", ThirdName = "هاشم", LastName = "الزيود", PhoneNumber = "0782211445", IsWhatsapp = false, IsContactNumber = true, DateOfBirth = new DateTime(1996, 6, 15), Gender = Gender.Female, BloodType = BloodType.APositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالبة", Image = null },

             new Person { Id = 17, FirstName = "إيهاب", SecondName = "سامي", ThirdName = "سعيد", LastName = "العتوم", PhoneNumber = "0796655443", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1989, 9, 8), Gender = Gender.Male, BloodType = BloodType.BPositive, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "مبرمج", Image = null },

             new Person { Id = 18, FirstName = "جنان", SecondName = "طه", ThirdName = "رياض", LastName = "الطويل", PhoneNumber = "0787766554", IsWhatsapp = true, IsContactNumber = false, DateOfBirth = new DateTime(2002, 2, 14), Gender = Gender.Female, BloodType = BloodType.OPositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Student, Job = "طالبة", Image = null },

             new Person { Id = 19, FirstName = "سليمان", SecondName = "أكرم", ThirdName = "منير", LastName = "الحوري", PhoneNumber = "0794433556", IsWhatsapp = true, IsContactNumber = true, DateOfBirth = new DateTime(1984, 12, 3), Gender = Gender.Male, BloodType = BloodType.ABNegative, MaritalStatus = MaritalStatus.Married, OccupationStatus = OccupationStatus.Employee, Job = "مقاول", Image = null },

             new Person { Id = 20, FirstName = "تقى", SecondName = "نائل", ThirdName = "فارس", LastName = "الصمادي", PhoneNumber = "0789988771", IsWhatsapp = false, IsContactNumber = true, DateOfBirth = new DateTime(1999, 5, 6), Gender = Gender.Female, BloodType = BloodType.BPositive, MaritalStatus = MaritalStatus.Single, OccupationStatus = OccupationStatus.Employee, Job = "سكرتيرة", Image = null }
         );
        }
    }
}
