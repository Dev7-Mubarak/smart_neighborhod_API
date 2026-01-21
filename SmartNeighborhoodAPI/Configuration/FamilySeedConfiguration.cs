using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurProjectSmartNeiborhood.Entites;
using SmartNeighborhoodAPI.Entites.Enums;

namespace SmartNeighborhoodAPI.Configuration
{
    public class FamilySeedConfiguration 
        : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            // Seed 20 families across the blocks for testing
            builder.HasData(
                // Block 1 - 2 families
                new Family { Id = 1, Name = "عائلة الغانم", Location = "البيت 1، الحي القديم", FamilyNotes = "لا ملاحظات", FamilyCatgoryId = 1, HousingType = HousingType.Property, BlockId = 1 },
                new Family { Id = 2, Name = "عائلة الملاح", Location = "البيت 2، شارع البحر", FamilyNotes = "تأجير طويل الأمد", FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 1 },

                // Block 2 - 2 families
                new Family { Id = 3, Name = "عائلة العكبري", Location = "الزاوية الجنوبية، مبنى 2", FamilyNotes = "عائلة مع العديد من الأولاد", FamilyCatgoryId = 2, HousingType = HousingType.Property, BlockId = 2 },
                new Family { Id = 4, Name = "عائلة العمري", Location = "الطابق الأول، مبنى 2", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 2 },

                // Block 3 - 2 families
                new Family { Id = 5, Name = "عائلة الهاشمي", Location = "الحي الشمالي، مبنى 3", FamilyNotes = "تحتاج دعم صحّي", FamilyCatgoryId = 1, HousingType = HousingType.Property, BlockId = 3 },
                new Family { Id = 6, Name = "عائلة الحمادي", Location = "الطابق الثاني، مبنى 3", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 3 },

                // Block 4 - 2 families
                new Family { Id = 7, Name = "عائلة الحدري", Location = "الشارع العام، مبنى 4", FamilyNotes = "أحد أفراد الأسرة بحاجة لرعاية", FamilyCatgoryId = 2, HousingType = HousingType.Property, BlockId = 4 },
                new Family { Id = 8, Name = "عائلة البساطي", Location = "الصفحة الشرقية، مبنى 4", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 4 },

                // Block 5 - 2 families
                new Family { Id = 9, Name = "عائلة الملاح", Location = "المنطقة الجنوبية، مبنى 5", FamilyNotes = "عائلة مستقرة", FamilyCatgoryId = 1, HousingType = HousingType.Property, BlockId = 5 },
                new Family { Id = 10, Name = "عائلة القحطاني", Location = "الطابق الأرضي، مبنى 5", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 5 },

                // Additional families (11-20)
                new Family { Id = 11, Name = "عائلة الشريف", Location = "الطابق الأول، مبنى 6", FamilyNotes = null, FamilyCatgoryId = 1, HousingType = HousingType.Property, BlockId = 6 },
                new Family { Id = 12, Name = "عائلة الزبيري", Location = "البيت 2، مبنى 7", FamilyNotes = "تحتاج دعم تعليمي", FamilyCatgoryId = 2, HousingType = HousingType.Rent, BlockId = 7 },
                new Family { Id = 13, Name = "عائلة الهاشمي", Location = "الزاوية الغربية، مبنى 8", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Property, BlockId = 8 },
                new Family { Id = 14, Name = "عائلة العرادي", Location = "الشقة 3، مبنى 9", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 9 },
                new Family { Id = 15, Name = "عائلة القاضي", Location = "الطابق العلوي، مبنى 10", FamilyNotes = "أم عازبة", FamilyCatgoryId = 1, HousingType = HousingType.Property, BlockId = 10 },
                new Family { Id = 16, Name = "عائلة الربيعي", Location = "مبنى 11، شارع السوق", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 11 },
                new Family { Id = 17, Name = "عائلة الحاج", Location = "المنطقة 12، مبنى 12", FamilyNotes = "عائلة كبيرة", FamilyCatgoryId = 2, HousingType = HousingType.Property, BlockId = 12 },
                new Family { Id = 18, Name = "عائلة السقاف", Location = "الطابق الثاني، مبنى 13", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Rent, BlockId = 13 },
                new Family { Id = 19, Name = "عائلة العولقي", Location = "منزل صغير، مبنى 14", FamilyNotes = null, FamilyCatgoryId = 3, HousingType = HousingType.Property, BlockId = 14 },
                new Family { Id = 20, Name = "عائلة مختار", Location = "الطابق الأرضي، مبنى 15", FamilyNotes = null, FamilyCatgoryId = 2, HousingType = HousingType.Rent, BlockId = 15 }
            );
        }
    }
}
