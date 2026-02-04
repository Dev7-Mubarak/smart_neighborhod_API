//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OurProjectSmartNeiborhood.Entites;
//using SmartNeighborhoodAPI.Entites;

//public class ResidentialNeighborhoodSeedConfiguration : IEntityTypeConfiguration<ResidentialNeighborhood>
//{
//    public void Configure(EntityTypeBuilder<ResidentialNeighborhood> builder)
//    {
//        builder.HasData(
//            new ResidentialNeighborhood { Id = 1, Name = "›·ﬂ", NeighborhoodManagerId = "00000000000000000000000000000001" },
//            new ResidentialNeighborhood { Id = 2, Name = "«·ﬁ«—… «·Õ„—«¡", NeighborhoodManagerId = "00000000000000000000000000000002" },
//            new ResidentialNeighborhood { Id = 3, Name = "«·⁄Ì’", NeighborhoodManagerId = "00000000000000000000000000000003" },
//            new ResidentialNeighborhood { Id = 4, Name = "»ÊÌ‘ «·ﬁœÌ„…", NeighborhoodManagerId = "00000000000000000000000000000004" },
//            new ResidentialNeighborhood { Id = 5, Name = "»ÊÌ‘ «·ÃœÌœÂ", NeighborhoodManagerId = "00000000000000000000000000000005" },
//            new ResidentialNeighborhood { Id = 6, Name = "»ÊÌ‘ / —Êﬂ»", NeighborhoodManagerId = "00000000000000000000000000000006" },
//            new ResidentialNeighborhood { Id = 7, Name = "«·⁄Ê”Ã…", NeighborhoodManagerId = "00000000000000000000000000000007" },
//            new ResidentialNeighborhood { Id = 8, Name = "—Êﬂ» «·ﬁœÌ„…", NeighborhoodManagerId = "00000000000000000000000000000008" },
//            new ResidentialNeighborhood { Id = 9, Name = "«·„€ —»Ì‰", NeighborhoodManagerId = "00000000000000000000000000000009" },
//            new ResidentialNeighborhood { Id = 10, Name = "«·‰›ÿ", NeighborhoodManagerId = "0000000000000000000000000000000a" },
//            new ResidentialNeighborhood { Id = 11, Name = "«·Õ—‘Ì« ", NeighborhoodManagerId = "0000000000000000000000000000000b" },
//            new ResidentialNeighborhood { Id = 12, Name = "ÃÊ· „”Õ…", NeighborhoodManagerId = "0000000000000000000000000000000c" },
//            new ResidentialNeighborhood { Id = 13, Name = "—»Ê… Œ·›", NeighborhoodManagerId = "0000000000000000000000000000000d" },
//            new ResidentialNeighborhood { Id = 14, Name = "Œ·›", NeighborhoodManagerId = "0000000000000000000000000000000e" },
//            new ResidentialNeighborhood { Id = 15, Name = "«·‘ÂÌœ", NeighborhoodManagerId = "0000000000000000000000000000000f" },
//            new ResidentialNeighborhood { Id = 16, Name = "«·Õ«—…", NeighborhoodManagerId = "00000000000000000000000000000010" },
//            new ResidentialNeighborhood { Id = 17, Name = "«·”·«„", NeighborhoodManagerId = "00000000000000000000000000000011" },
//            new ResidentialNeighborhood { Id = 18, Name = "»œÌ—Ì", NeighborhoodManagerId = "00000000000000000000000000000012" },
//            new ResidentialNeighborhood { Id = 19, Name = "«ﬂ Ê»—", NeighborhoodManagerId = "00000000000000000000000000000013" },
//            new ResidentialNeighborhood { Id = 20, Name = "‘⁄» «·»«œÌ…", NeighborhoodManagerId = "00000000000000000000000000000014" },
//            new ResidentialNeighborhood { Id = 21, Name = "«·Õ”Ì‰Ì", NeighborhoodManagerId = "00000000000000000000000000000015" },
//            new ResidentialNeighborhood { Id = 22, Name = "30 ‰Ê›„»—", NeighborhoodManagerId = "00000000000000000000000000000016" },
//            new ResidentialNeighborhood { Id = 23, Name = "»«”ÊÌœ", NeighborhoodManagerId = "00000000000000000000000000000017" },
//            new ResidentialNeighborhood { Id = 24, Name = "«·›«—Êﬁ", NeighborhoodManagerId = "00000000000000000000000000000018" },
//            new ResidentialNeighborhood { Id = 25, Name = "ÃÊ· «·‘›«¡", NeighborhoodManagerId = "00000000000000000000000000000019" },
//            new ResidentialNeighborhood { Id = 26, Name = "«·Õ’‰", NeighborhoodManagerId = "0000000000000000000000000000001a" },
//            new ResidentialNeighborhood { Id = 27, Name = "«·ÀÊ—…", NeighborhoodManagerId = "0000000000000000000000000000001b" },
//            new ResidentialNeighborhood { Id = 28, Name = "«·⁄„«·", NeighborhoodManagerId = "0000000000000000000000000000001c" },
//            new ResidentialNeighborhood { Id = 29, Name = "«·«” ﬁ·«·", NeighborhoodManagerId = "0000000000000000000000000000001d" },
//            new ResidentialNeighborhood { Id = 30, Name = "«·‰’—", NeighborhoodManagerId = "0000000000000000000000000000001e" },
//            new ResidentialNeighborhood { Id = 31, Name = "40 ‘ﬁ…", NeighborhoodManagerId = "0000000000000000000000000000001f" },
//            new ResidentialNeighborhood { Id = 32, Name = "€—» «·÷Ì«›…", NeighborhoodManagerId = "00000000000000000000000000000020" },
//            new ResidentialNeighborhood { Id = 33, Name = "«·«‰‘«¡  √„»ÌŒ…", NeighborhoodManagerId = "00000000000000000000000000000021" },
//            new ResidentialNeighborhood { Id = 34, Name = "«·√‰‘«¡ ", NeighborhoodManagerId = "00000000000000000000000000000022" },
//            new ResidentialNeighborhood { Id = 35, Name = "»«“‰»Ê—", NeighborhoodManagerId = "00000000000000000000000000000023" },
//            new ResidentialNeighborhood { Id = 36, Name = "«·„” ﬁ»·", NeighborhoodManagerId = "00000000000000000000000000000024" },
//            new ResidentialNeighborhood { Id = 37, Name = "«·„”«ﬂ‰", NeighborhoodManagerId = "00000000000000000000000000000025" },
//            new ResidentialNeighborhood { Id = 38, Name = "«·ÿ·«∆⁄", NeighborhoodManagerId = "00000000000000000000000000000026" },
//            new ResidentialNeighborhood { Id = 39, Name = "«· ⁄«Ê‰", NeighborhoodManagerId = "00000000000000000000000000000027" },
//            new ResidentialNeighborhood { Id = 40, Name = "«·ÿÊÌ·…", NeighborhoodManagerId = "00000000000000000000000000000028" },
//            new ResidentialNeighborhood { Id = 41, Name = "«»‰ ”Ì‰«¡", NeighborhoodManagerId = "00000000000000000000000000000029" },
//            new ResidentialNeighborhood { Id = 42, Name = "«·‘«›⁄Ì", NeighborhoodManagerId = "0000000000000000000000000000002a" },
//            new ResidentialNeighborhood { Id = 43, Name = "1 „«ÌÊ", NeighborhoodManagerId = "0000000000000000000000000000002b" },
//            new ResidentialNeighborhood { Id = 44, Name = "Œ„—", NeighborhoodManagerId = "0000000000000000000000000000002c" },
//            new ResidentialNeighborhood { Id = 45, Name = "€—Ì—", NeighborhoodManagerId = "0000000000000000000000000000002d" },
//            new ResidentialNeighborhood { Id = 46, Name = "Õ·…", NeighborhoodManagerId = "0000000000000000000000000000002e" }
//        );
//    }
//}
