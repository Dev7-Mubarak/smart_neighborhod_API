//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using OurProjectSmartNeiborhood.Entites;
//using SmartNeighborhoodAPI.Entites;

//public class ResidentialUnitSeedConfiguration : IEntityTypeConfiguration<ResidentialUnit>
//{
//    public void Configure(EntityTypeBuilder<ResidentialUnit> builder)
//    {
//        builder.HasData(
//            new ResidentialUnit
//            {
//                Id = 1,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 1,
//                UnitManagerId = "0000000000000000000000000000002f"
//            },
//            new ResidentialUnit
//            {
//                Id = 2,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 1,
//                UnitManagerId = "00000000000000000000000000000030"
//            },
//            new ResidentialUnit
//            {
//                Id = 3,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 1,
//                UnitManagerId = "00000000000000000000000000000031"
//            },
//            new ResidentialUnit
//            {
//                Id = 4,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 1,
//                UnitManagerId = "00000000000000000000000000000032"
//            },
//            new ResidentialUnit
//            {
//                Id = 5,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 1,
//                UnitManagerId = "00000000000000000000000000000033"
//            },
//            new ResidentialUnit
//            {
//                Id = 6,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 2,
//                UnitManagerId = "00000000000000000000000000000034"
//            },
//            new ResidentialUnit
//            {
//                Id = 7,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 2,
//                UnitManagerId = "00000000000000000000000000000035"
//            },
//            new ResidentialUnit
//            {
//                Id = 8,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 2,
//                UnitManagerId = "00000000000000000000000000000036"
//            },
//            new ResidentialUnit
//            {
//                Id = 9,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 2,
//                UnitManagerId = "00000000000000000000000000000037"
//            },
//            new ResidentialUnit
//            {
//                Id = 10,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 2,
//                UnitManagerId = "00000000000000000000000000000038"
//            },
//            new ResidentialUnit
//            {
//                Id = 11,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 3,
//                UnitManagerId = "00000000000000000000000000000039"
//            },
//            new ResidentialUnit
//            {
//                Id = 12,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 3,
//                UnitManagerId = "0000000000000000000000000000003a"
//            },
//            new ResidentialUnit
//            {
//                Id = 13,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 3,
//                UnitManagerId = "0000000000000000000000000000003b"
//            },
//            new ResidentialUnit
//            {
//                Id = 14,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 3,
//                UnitManagerId = "0000000000000000000000000000003c"
//            },
//            new ResidentialUnit
//            {
//                Id = 15,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 3,
//                UnitManagerId = "0000000000000000000000000000003d"
//            },
//            new ResidentialUnit
//            {
//                Id = 16,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 4,
//                UnitManagerId = "0000000000000000000000000000003e"
//            },
//            new ResidentialUnit
//            {
//                Id = 17,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 4,
//                UnitManagerId = "0000000000000000000000000000003f"
//            },
//            new ResidentialUnit
//            {
//                Id = 18,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 4,
//                UnitManagerId = "00000000000000000000000000000040"
//            },
//            new ResidentialUnit
//            {
//                Id = 19,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 4,
//                UnitManagerId = "00000000000000000000000000000041"
//            },
//            new ResidentialUnit
//            {
//                Id = 20,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 4,
//                UnitManagerId = "00000000000000000000000000000042"
//            },
//            new ResidentialUnit
//            {
//                Id = 21,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 5,
//                UnitManagerId = "00000000000000000000000000000043"
//            },
//            new ResidentialUnit
//            {
//                Id = 22,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 5,
//                UnitManagerId = "00000000000000000000000000000044"
//            },
//            new ResidentialUnit
//            {
//                Id = 23,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 5,
//                UnitManagerId = "00000000000000000000000000000045"
//            },
//            new ResidentialUnit
//            {
//                Id = 24,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 5,
//                UnitManagerId = "00000000000000000000000000000046"
//            },
//            new ResidentialUnit
//            {
//                Id = 25,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 5,
//                UnitManagerId = "00000000000000000000000000000047"
//            },
//            new ResidentialUnit
//            {
//                Id = 26,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 6,
//                UnitManagerId = "00000000000000000000000000000048"
//            },
//            new ResidentialUnit
//            {
//                Id = 27,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 6,
//                UnitManagerId = "00000000000000000000000000000049"
//            },
//            new ResidentialUnit
//            {
//                Id = 28,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 6,
//                UnitManagerId = "0000000000000000000000000000004a"
//            },
//            new ResidentialUnit
//            {
//                Id = 29,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 6,
//                UnitManagerId = "0000000000000000000000000000004b"
//            },
//            new ResidentialUnit
//            {
//                Id = 30,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 6,
//                UnitManagerId = "0000000000000000000000000000004c"
//            },
//            new ResidentialUnit
//            {
//                Id = 31,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 7,
//                UnitManagerId = "0000000000000000000000000000004d"
//            },
//            new ResidentialUnit
//            {
//                Id = 32,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 7,
//                UnitManagerId = "0000000000000000000000000000004e"
//            },
//            new ResidentialUnit
//            {
//                Id = 33,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 7,
//                UnitManagerId = "0000000000000000000000000000004f"
//            },
//            new ResidentialUnit
//            {
//                Id = 34,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 7,
//                UnitManagerId = "00000000000000000000000000000050"
//            },
//            new ResidentialUnit
//            {
//                Id = 35,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 7,
//                UnitManagerId = "00000000000000000000000000000051"
//            },
//            new ResidentialUnit
//            {
//                Id = 36,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 8,
//                UnitManagerId = "00000000000000000000000000000052"
//            },
//            new ResidentialUnit
//            {
//                Id = 37,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 8,
//                UnitManagerId = "00000000000000000000000000000053"
//            },
//            new ResidentialUnit
//            {
//                Id = 38,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 8,
//                UnitManagerId = "00000000000000000000000000000054"
//            },
//            new ResidentialUnit
//            {
//                Id = 39,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 8,
//                UnitManagerId = "00000000000000000000000000000055"
//            },
//            new ResidentialUnit
//            {
//                Id = 40,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 8,
//                UnitManagerId = "00000000000000000000000000000056"
//            },
//            new ResidentialUnit
//            {
//                Id = 41,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 9,
//                UnitManagerId = "00000000000000000000000000000057"
//            },
//            new ResidentialUnit
//            {
//                Id = 42,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 9,
//                UnitManagerId = "00000000000000000000000000000058"
//            },
//            new ResidentialUnit
//            {
//                Id = 43,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 9,
//                UnitManagerId = "00000000000000000000000000000059"
//            },
//            new ResidentialUnit
//            {
//                Id = 44,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 9,
//                UnitManagerId = "0000000000000000000000000000005a"
//            },
//            new ResidentialUnit
//            {
//                Id = 45,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 9,
//                UnitManagerId = "0000000000000000000000000000005b"
//            },
//            new ResidentialUnit
//            {
//                Id = 46,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 10,
//                UnitManagerId = "0000000000000000000000000000005c"
//            },
//            new ResidentialUnit
//            {
//                Id = 47,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 10,
//                UnitManagerId = "0000000000000000000000000000005d"
//            },
//            new ResidentialUnit
//            {
//                Id = 48,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 10,
//                UnitManagerId = "0000000000000000000000000000005e"
//            },
//            new ResidentialUnit
//            {
//                Id = 49,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 10,
//                UnitManagerId = "0000000000000000000000000000005f"
//            },
//            new ResidentialUnit
//            {
//                Id = 50,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 10,
//                UnitManagerId = "00000000000000000000000000000060"
//            },
//            new ResidentialUnit
//            {
//                Id = 51,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 11,
//                UnitManagerId = "00000000000000000000000000000061"
//            },
//            new ResidentialUnit
//            {
//                Id = 52,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 11,
//                UnitManagerId = "00000000000000000000000000000062"
//            },
//            new ResidentialUnit
//            {
//                Id = 53,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 11,
//                UnitManagerId = "00000000000000000000000000000063"
//            },
//            new ResidentialUnit
//            {
//                Id = 54,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 11,
//                UnitManagerId = "00000000000000000000000000000064"
//            },
//            new ResidentialUnit
//            {
//                Id = 55,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 11,
//                UnitManagerId = "00000000000000000000000000000065"
//            },
//            new ResidentialUnit
//            {
//                Id = 56,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 12,
//                UnitManagerId = "00000000000000000000000000000066"
//            },
//            new ResidentialUnit
//            {
//                Id = 57,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 12,
//                UnitManagerId = "00000000000000000000000000000067"
//            },
//            new ResidentialUnit
//            {
//                Id = 58,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 12,
//                UnitManagerId = "00000000000000000000000000000068"
//            },
//            new ResidentialUnit
//            {
//                Id = 59,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 12,
//                UnitManagerId = "00000000000000000000000000000069"
//            },
//            new ResidentialUnit
//            {
//                Id = 60,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 12,
//                UnitManagerId = "0000000000000000000000000000006a"
//            },
//            new ResidentialUnit
//            {
//                Id = 61,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 13,
//                UnitManagerId = "0000000000000000000000000000006b"
//            },
//            new ResidentialUnit
//            {
//                Id = 62,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 13,
//                UnitManagerId = "0000000000000000000000000000006c"
//            },
//            new ResidentialUnit
//            {
//                Id = 63,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 13,
//                UnitManagerId = "0000000000000000000000000000006d"
//            },
//            new ResidentialUnit
//            {
//                Id = 64,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 13,
//                UnitManagerId = "0000000000000000000000000000006e"
//            },
//            new ResidentialUnit
//            {
//                Id = 65,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 13,
//                UnitManagerId = "0000000000000000000000000000006f"
//            },
//            new ResidentialUnit
//            {
//                Id = 66,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 14,
//                UnitManagerId = "00000000000000000000000000000070"
//            },
//            new ResidentialUnit
//            {
//                Id = 67,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 14,
//                UnitManagerId = "00000000000000000000000000000071"
//            },
//            new ResidentialUnit
//            {
//                Id = 68,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 14,
//                UnitManagerId = "00000000000000000000000000000072"
//            },
//            new ResidentialUnit
//            {
//                Id = 69,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 14,
//                UnitManagerId = "00000000000000000000000000000073"
//            },
//            new ResidentialUnit
//            {
//                Id = 70,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 14,
//                UnitManagerId = "00000000000000000000000000000074"
//            },
//            new ResidentialUnit
//            {
//                Id = 71,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 15,
//                UnitManagerId = "00000000000000000000000000000075"
//            },
//            new ResidentialUnit
//            {
//                Id = 72,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 15,
//                UnitManagerId = "00000000000000000000000000000076"
//            },
//            new ResidentialUnit
//            {
//                Id = 73,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 15,
//                UnitManagerId = "00000000000000000000000000000077"
//            },
//            new ResidentialUnit
//            {
//                Id = 74,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 15,
//                UnitManagerId = "00000000000000000000000000000078"
//            },
//            new ResidentialUnit
//            {
//                Id = 75,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 15,
//                UnitManagerId = "00000000000000000000000000000079"
//            },
//            new ResidentialUnit
//            {
//                Id = 76,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 16,
//                UnitManagerId = "0000000000000000000000000000007a"
//            },
//            new ResidentialUnit
//            {
//                Id = 77,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 16,
//                UnitManagerId = "0000000000000000000000000000007b"
//            },
//            new ResidentialUnit
//            {
//                Id = 78,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 16,
//                UnitManagerId = "0000000000000000000000000000007c"
//            },
//            new ResidentialUnit
//            {
//                Id = 79,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 16,
//                UnitManagerId = "0000000000000000000000000000007d"
//            },
//            new ResidentialUnit
//            {
//                Id = 80,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 16,
//                UnitManagerId = "0000000000000000000000000000007e"
//            },
//            new ResidentialUnit
//            {
//                Id = 81,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 17,
//                UnitManagerId = "0000000000000000000000000000007f"
//            },
//            new ResidentialUnit
//            {
//                Id = 82,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 17,
//                UnitManagerId = "00000000000000000000000000000080"
//            },
//            new ResidentialUnit
//            {
//                Id = 83,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 17,
//                UnitManagerId = "00000000000000000000000000000081"
//            },
//            new ResidentialUnit
//            {
//                Id = 84,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 17,
//                UnitManagerId = "00000000000000000000000000000082"
//            },
//            new ResidentialUnit
//            {
//                Id = 85,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 17,
//                UnitManagerId = "00000000000000000000000000000083"
//            },
//            new ResidentialUnit
//            {
//                Id = 86,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 18,
//                UnitManagerId = "00000000000000000000000000000084"
//            },
//            new ResidentialUnit
//            {
//                Id = 87,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 18,
//                UnitManagerId = "00000000000000000000000000000085"
//            },
//            new ResidentialUnit
//            {
//                Id = 88,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 18,
//                UnitManagerId = "00000000000000000000000000000086"
//            },
//            new ResidentialUnit
//            {
//                Id = 89,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 18,
//                UnitManagerId = "00000000000000000000000000000087"
//            },
//            new ResidentialUnit
//            {
//                Id = 90,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 18,
//                UnitManagerId = "00000000000000000000000000000088"
//            },
//            new ResidentialUnit
//            {
//                Id = 91,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 19,
//                UnitManagerId = "00000000000000000000000000000089"
//            },
//            new ResidentialUnit
//            {
//                Id = 92,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 19,
//                UnitManagerId = "0000000000000000000000000000008a"
//            },
//            new ResidentialUnit
//            {
//                Id = 93,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 19,
//                UnitManagerId = "0000000000000000000000000000008b"
//            },
//            new ResidentialUnit
//            {
//                Id = 94,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 19,
//                UnitManagerId = "0000000000000000000000000000008c"
//            },
//            new ResidentialUnit
//            {
//                Id = 95,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 19,
//                UnitManagerId = "0000000000000000000000000000008d"
//            },
//            new ResidentialUnit
//            {
//                Id = 96,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 20,
//                UnitManagerId = "0000000000000000000000000000008e"
//            },
//            new ResidentialUnit
//            {
//                Id = 97,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 20,
//                UnitManagerId = "0000000000000000000000000000008f"
//            },
//            new ResidentialUnit
//            {
//                Id = 98,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 20,
//                UnitManagerId = "00000000000000000000000000000090"
//            },
//            new ResidentialUnit
//            {
//                Id = 99,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 20,
//                UnitManagerId = "00000000000000000000000000000091"
//            },
//            new ResidentialUnit
//            {
//                Id = 100,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 20,
//                UnitManagerId = "00000000000000000000000000000092"
//            },
//            new ResidentialUnit
//            {
//                Id = 101,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 21,
//                UnitManagerId = "00000000000000000000000000000093"
//            },
//            new ResidentialUnit
//            {
//                Id = 102,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 21,
//                UnitManagerId = "00000000000000000000000000000094"
//            },
//            new ResidentialUnit
//            {
//                Id = 103,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 21,
//                UnitManagerId = "00000000000000000000000000000095"
//            },
//            new ResidentialUnit
//            {
//                Id = 104,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 21,
//                UnitManagerId = "00000000000000000000000000000096"
//            },
//            new ResidentialUnit
//            {
//                Id = 105,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 21,
//                UnitManagerId = "00000000000000000000000000000097"
//            },
//            new ResidentialUnit
//            {
//                Id = 106,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 22,
//                UnitManagerId = "00000000000000000000000000000098"
//            },
//            new ResidentialUnit
//            {
//                Id = 107,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 22,
//                UnitManagerId = "00000000000000000000000000000099"
//            },
//            new ResidentialUnit
//            {
//                Id = 108,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 22,
//                UnitManagerId = "0000000000000000000000000000009a"
//            },
//            new ResidentialUnit
//            {
//                Id = 109,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 22,
//                UnitManagerId = "0000000000000000000000000000009b"
//            },
//            new ResidentialUnit
//            {
//                Id = 110,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 22,
//                UnitManagerId = "0000000000000000000000000000009c"
//            },
//            new ResidentialUnit
//            {
//                Id = 111,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 23,
//                UnitManagerId = "0000000000000000000000000000009d"
//            },
//            new ResidentialUnit
//            {
//                Id = 112,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 23,
//                UnitManagerId = "0000000000000000000000000000009e"
//            },
//            new ResidentialUnit
//            {
//                Id = 113,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 23,
//                UnitManagerId = "0000000000000000000000000000009f"
//            },
//            new ResidentialUnit
//            {
//                Id = 114,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 23,
//                UnitManagerId = "000000000000000000000000000000a0"
//            },
//            new ResidentialUnit
//            {
//                Id = 115,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 23,
//                UnitManagerId = "000000000000000000000000000000a1"
//            },
//            new ResidentialUnit
//            {
//                Id = 116,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 24,
//                UnitManagerId = "000000000000000000000000000000a2"
//            },
//            new ResidentialUnit
//            {
//                Id = 117,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 24,
//                UnitManagerId = "000000000000000000000000000000a3"
//            },
//            new ResidentialUnit
//            {
//                Id = 118,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 24,
//                UnitManagerId = "000000000000000000000000000000a4"
//            },
//            new ResidentialUnit
//            {
//                Id = 119,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 24,
//                UnitManagerId = "000000000000000000000000000000a5"
//            },
//            new ResidentialUnit
//            {
//                Id = 120,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 24,
//                UnitManagerId = "000000000000000000000000000000a6"
//            },
//            new ResidentialUnit
//            {
//                Id = 121,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 25,
//                UnitManagerId = "000000000000000000000000000000a7"
//            },
//            new ResidentialUnit
//            {
//                Id = 122,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 25,
//                UnitManagerId = "000000000000000000000000000000a8"
//            },
//            new ResidentialUnit
//            {
//                Id = 123,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 25,
//                UnitManagerId = "000000000000000000000000000000a9"
//            },
//            new ResidentialUnit
//            {
//                Id = 124,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 25,
//                UnitManagerId = "000000000000000000000000000000aa"
//            },
//            new ResidentialUnit
//            {
//                Id = 125,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 25,
//                UnitManagerId = "000000000000000000000000000000ab"
//            },
//            new ResidentialUnit
//            {
//                Id = 126,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 26,
//                UnitManagerId = "000000000000000000000000000000ac"
//            },
//            new ResidentialUnit
//            {
//                Id = 127,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 26,
//                UnitManagerId = "000000000000000000000000000000ad"
//            },
//            new ResidentialUnit
//            {
//                Id = 128,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 26,
//                UnitManagerId = "000000000000000000000000000000ae"
//            },
//            new ResidentialUnit
//            {
//                Id = 129,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 26,
//                UnitManagerId = "000000000000000000000000000000af"
//            },
//            new ResidentialUnit
//            {
//                Id = 130,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 26,
//                UnitManagerId = "000000000000000000000000000000b0"
//            },
//            new ResidentialUnit
//            {
//                Id = 131,
//                Name = "وحدة التضامن",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b1"
//            },
//            new ResidentialUnit
//            {
//                Id = 132,
//                Name = "وحدة المنقد",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b2"
//            },
//            new ResidentialUnit
//            {
//                Id = 133,
//                Name = "وحدة بارجاش",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b3"
//            },
//            new ResidentialUnit
//            {
//                Id = 134,
//                Name = "وحدة الخزان",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b4"
//            },
//            new ResidentialUnit
//            {
//                Id = 135,
//                Name = "وحدة الشفع",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b5"
//            },
//            new ResidentialUnit
//            {
//                Id = 136,
//                Name = "وحدة الزراعة",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b6"
//            },
//            new ResidentialUnit
//            {
//                Id = 137,
//                Name = "وحدة الصفا",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b7"
//            },
//            new ResidentialUnit
//            {
//                Id = 138,
//                Name = "وحدة الغار الأحمر",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000b8"
//            },
//            new ResidentialUnit
//            {
//                Id = 139,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 28,
//                UnitManagerId = "000000000000000000000000000000b9"
//            },
//            new ResidentialUnit
//            {
//                Id = 140,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 28,
//                UnitManagerId = "000000000000000000000000000000ba"
//            },
//            new ResidentialUnit
//            {
//                Id = 141,
//                Name = "وحدة باعبود",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000bb"
//            },
//            new ResidentialUnit
//            {
//                Id = 142,
//                Name = "وحدة بادبيس",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000bc"
//            },
//            new ResidentialUnit
//            {
//                Id = 143,
//                Name = "وحدة جبل باعبود",
//                ResidentialNeighborhoodId = 27,
//                UnitManagerId = "000000000000000000000000000000bd"
//            },
//            new ResidentialUnit
//            {
//                Id = 144,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 29,
//                UnitManagerId = "000000000000000000000000000000be"
//            },
//            new ResidentialUnit
//            {
//                Id = 145,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 29,
//                UnitManagerId = "000000000000000000000000000000bf"
//            },
//            new ResidentialUnit
//            {
//                Id = 146,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 30,
//                UnitManagerId = "000000000000000000000000000000c0"
//            },
//            new ResidentialUnit
//            {
//                Id = 147,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 30,
//                UnitManagerId = "000000000000000000000000000000c1"
//            },
//            new ResidentialUnit
//            {
//                Id = 148,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 30,
//                UnitManagerId = "000000000000000000000000000000c2"
//            },
//            new ResidentialUnit
//            {
//                Id = 149,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 30,
//                UnitManagerId = "000000000000000000000000000000c3"
//            },
//            new ResidentialUnit
//            {
//                Id = 150,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 30,
//                UnitManagerId = "000000000000000000000000000000c4"
//            },
//            new ResidentialUnit
//            {
//                Id = 151,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 31,
//                UnitManagerId = "000000000000000000000000000000c5"
//            },
//            new ResidentialUnit
//            {
//                Id = 152,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 31,
//                UnitManagerId = "000000000000000000000000000000c6"
//            },
//            new ResidentialUnit
//            {
//                Id = 153,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 31,
//                UnitManagerId = "000000000000000000000000000000c7"
//            },
//            new ResidentialUnit
//            {
//                Id = 154,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 31,
//                UnitManagerId = "000000000000000000000000000000c8"
//            },
//            new ResidentialUnit
//            {
//                Id = 155,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 31,
//                UnitManagerId = "000000000000000000000000000000c9"
//            },
//            new ResidentialUnit
//            {
//                Id = 156,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 32,
//                UnitManagerId = "000000000000000000000000000000ca"
//            },
//            new ResidentialUnit
//            {
//                Id = 157,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 32,
//                UnitManagerId = "000000000000000000000000000000cb"
//            },
//            new ResidentialUnit
//            {
//                Id = 158,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 32,
//                UnitManagerId = "000000000000000000000000000000cc"
//            },
//            new ResidentialUnit
//            {
//                Id = 159,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 32,
//                UnitManagerId = "000000000000000000000000000000cd"
//            },
//            new ResidentialUnit
//            {
//                Id = 160,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 32,
//                UnitManagerId = "000000000000000000000000000000ce"
//            },
//            new ResidentialUnit
//            {
//                Id = 161,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 33,
//                UnitManagerId = "000000000000000000000000000000cf"
//            },
//            new ResidentialUnit
//            {
//                Id = 162,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 33,
//                UnitManagerId = "000000000000000000000000000000d0"
//            },
//            new ResidentialUnit
//            {
//                Id = 163,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 33,
//                UnitManagerId = "000000000000000000000000000000d1"
//            },
//            new ResidentialUnit
//            {
//                Id = 164,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 33,
//                UnitManagerId = "000000000000000000000000000000d2"
//            },
//            new ResidentialUnit
//            {
//                Id = 165,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 33,
//                UnitManagerId = "000000000000000000000000000000d3"
//            },
//            new ResidentialUnit
//            {
//                Id = 166,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 34,
//                UnitManagerId = "000000000000000000000000000000d4"
//            },
//            new ResidentialUnit
//            {
//                Id = 167,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 34,
//                UnitManagerId = "000000000000000000000000000000d5"
//            },
//            new ResidentialUnit
//            {
//                Id = 168,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 34,
//                UnitManagerId = "000000000000000000000000000000d6"
//            },
//            new ResidentialUnit
//            {
//                Id = 169,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 34,
//                UnitManagerId = "000000000000000000000000000000d7"
//            },
//            new ResidentialUnit
//            {
//                Id = 170,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 34,
//                UnitManagerId = "000000000000000000000000000000d8"
//            },
//            new ResidentialUnit
//            {
//                Id = 171,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 35,
//                UnitManagerId = "000000000000000000000000000000d9"
//            },
//            new ResidentialUnit
//            {
//                Id = 172,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 35,
//                UnitManagerId = "000000000000000000000000000000da"
//            },
//            new ResidentialUnit
//            {
//                Id = 173,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 35,
//                UnitManagerId = "000000000000000000000000000000db"
//            },
//            new ResidentialUnit
//            {
//                Id = 174,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 35,
//                UnitManagerId = "000000000000000000000000000000dc"
//            },
//            new ResidentialUnit
//            {
//                Id = 175,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 35,
//                UnitManagerId = "000000000000000000000000000000dd"
//            },
//            new ResidentialUnit
//            {
//                Id = 176,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 36,
//                UnitManagerId = "000000000000000000000000000000de"
//            },
//            new ResidentialUnit
//            {
//                Id = 177,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 36,
//                UnitManagerId = "000000000000000000000000000000df"
//            },
//            new ResidentialUnit
//            {
//                Id = 178,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 36,
//                UnitManagerId = "000000000000000000000000000000e0"
//            },
//            new ResidentialUnit
//            {
//                Id = 179,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 36,
//                UnitManagerId = "000000000000000000000000000000e1"
//            },
//            new ResidentialUnit
//            {
//                Id = 180,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 36,
//                UnitManagerId = "000000000000000000000000000000e2"
//            },
//            new ResidentialUnit
//            {
//                Id = 181,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 37,
//                UnitManagerId = "000000000000000000000000000000e3"
//            },
//            new ResidentialUnit
//            {
//                Id = 182,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 37,
//                UnitManagerId = "000000000000000000000000000000e4"
//            },
//            new ResidentialUnit
//            {
//                Id = 183,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 37,
//                UnitManagerId = "000000000000000000000000000000e5"
//            },
//            new ResidentialUnit
//            {
//                Id = 184,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 37,
//                UnitManagerId = "000000000000000000000000000000e6"
//            },
//            new ResidentialUnit
//            {
//                Id = 185,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 37,
//                UnitManagerId = "000000000000000000000000000000e7"
//            },
//            new ResidentialUnit
//            {
//                Id = 186,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 38,
//                UnitManagerId = "000000000000000000000000000000e8"
//            },
//            new ResidentialUnit
//            {
//                Id = 187,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 38,
//                UnitManagerId = "000000000000000000000000000000e9"
//            },
//            new ResidentialUnit
//            {
//                Id = 188,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 38,
//                UnitManagerId = "000000000000000000000000000000ea"
//            },
//            new ResidentialUnit
//            {
//                Id = 189,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 38,
//                UnitManagerId = "000000000000000000000000000000eb"
//            },
//            new ResidentialUnit
//            {
//                Id = 190,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 38,
//                UnitManagerId = "000000000000000000000000000000ec"
//            },
//            new ResidentialUnit
//            {
//                Id = 191,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 39,
//                UnitManagerId = "000000000000000000000000000000ed"
//            },
//            new ResidentialUnit
//            {
//                Id = 192,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 39,
//                UnitManagerId = "000000000000000000000000000000ee"
//            },
//            new ResidentialUnit
//            {
//                Id = 193,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 39,
//                UnitManagerId = "000000000000000000000000000000ef"
//            },
//            new ResidentialUnit
//            {
//                Id = 194,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 39,
//                UnitManagerId = "000000000000000000000000000000f0"
//            },
//            new ResidentialUnit
//            {
//                Id = 195,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 39,
//                UnitManagerId = "000000000000000000000000000000f1"
//            },
//            new ResidentialUnit
//            {
//                Id = 196,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 40,
//                UnitManagerId = "000000000000000000000000000000f2"
//            },
//            new ResidentialUnit
//            {
//                Id = 197,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 40,
//                UnitManagerId = "000000000000000000000000000000f3"
//            },
//            new ResidentialUnit
//            {
//                Id = 198,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 40,
//                UnitManagerId = "000000000000000000000000000000f4"
//            },
//            new ResidentialUnit
//            {
//                Id = 199,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 40,
//                UnitManagerId = "000000000000000000000000000000f5"
//            },
//            new ResidentialUnit
//            {
//                Id = 200,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 40,
//                UnitManagerId = "000000000000000000000000000000f6"
//            },
//            new ResidentialUnit
//            {
//                Id = 201,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 41,
//                UnitManagerId = "000000000000000000000000000000f7"
//            },
//            new ResidentialUnit
//            {
//                Id = 202,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 41,
//                UnitManagerId = "000000000000000000000000000000f8"
//            },
//            new ResidentialUnit
//            {
//                Id = 203,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 41,
//                UnitManagerId = "000000000000000000000000000000f9"
//            },
//            new ResidentialUnit
//            {
//                Id = 204,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 41,
//                UnitManagerId = "000000000000000000000000000000fa"
//            },
//            new ResidentialUnit
//            {
//                Id = 205,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 41,
//                UnitManagerId = "000000000000000000000000000000fb"
//            },
//            new ResidentialUnit
//            {
//                Id = 206,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 42,
//                UnitManagerId = "000000000000000000000000000000fc"
//            },
//            new ResidentialUnit
//            {
//                Id = 207,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 42,
//                UnitManagerId = "000000000000000000000000000000fd"
//            },
//            new ResidentialUnit
//            {
//                Id = 208,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 42,
//                UnitManagerId = "000000000000000000000000000000fe"
//            },
//            new ResidentialUnit
//            {
//                Id = 209,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 42,
//                UnitManagerId = "000000000000000000000000000000ff"
//            },
//            new ResidentialUnit
//            {
//                Id = 210,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 42,
//                UnitManagerId = "00000000000000000000000000000100"
//            },
//            new ResidentialUnit
//            {
//                Id = 211,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 43,
//                UnitManagerId = "00000000000000000000000000000101"
//            },
//            new ResidentialUnit
//            {
//                Id = 212,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 43,
//                UnitManagerId = "00000000000000000000000000000102"
//            },
//            new ResidentialUnit
//            {
//                Id = 213,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 43,
//                UnitManagerId = "00000000000000000000000000000103"
//            },
//            new ResidentialUnit
//            {
//                Id = 214,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 43,
//                UnitManagerId = "00000000000000000000000000000104"
//            },
//            new ResidentialUnit
//            {
//                Id = 215,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 43,
//                UnitManagerId = "00000000000000000000000000000105"
//            },
//            new ResidentialUnit
//            {
//                Id = 216,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 44,
//                UnitManagerId = "00000000000000000000000000000106"
//            },
//            new ResidentialUnit
//            {
//                Id = 217,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 44,
//                UnitManagerId = "00000000000000000000000000000107"
//            },
//            new ResidentialUnit
//            {
//                Id = 218,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 44,
//                UnitManagerId = "00000000000000000000000000000108"
//            },
//            new ResidentialUnit
//            {
//                Id = 219,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 44,
//                UnitManagerId = "00000000000000000000000000000109"
//            },
//            new ResidentialUnit
//            {
//                Id = 220,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 44,
//                UnitManagerId = "0000000000000000000000000000010a"
//            },
//            new ResidentialUnit
//            {
//                Id = 221,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 45,
//                UnitManagerId = "0000000000000000000000000000010b"
//            },
//            new ResidentialUnit
//            {
//                Id = 222,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 45,
//                UnitManagerId = "0000000000000000000000000000010c"
//            },
//            new ResidentialUnit
//            {
//                Id = 223,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 45,
//                UnitManagerId = "0000000000000000000000000000010d"
//            },
//            new ResidentialUnit
//            {
//                Id = 224,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 45,
//                UnitManagerId = "0000000000000000000000000000010e"
//            },
//            new ResidentialUnit
//            {
//                Id = 225,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 45,
//                UnitManagerId = "0000000000000000000000000000010f"
//            },
//            new ResidentialUnit
//            {
//                Id = 226,
//                Name = "وحدة 1",
//                ResidentialNeighborhoodId = 46,
//                UnitManagerId = "00000000000000000000000000000110"
//            },
//            new ResidentialUnit
//            {
//                Id = 227,
//                Name = "وحدة 2",
//                ResidentialNeighborhoodId = 46,
//                UnitManagerId = "00000000000000000000000000000111"
//            },
//            new ResidentialUnit
//            {
//                Id = 228,
//                Name = "وحدة 3",
//                ResidentialNeighborhoodId = 46,
//                UnitManagerId = "00000000000000000000000000000112"
//            },
//            new ResidentialUnit
//            {
//                Id = 229,
//                Name = "وحدة 4",
//                ResidentialNeighborhoodId = 46,
//                UnitManagerId = "00000000000000000000000000000113"
//            },
//            new ResidentialUnit
//            {
//                Id = 230,
//                Name = "وحدة 5",
//                ResidentialNeighborhoodId = 46,
//                UnitManagerId = "00000000000000000000000000000114"
//            }
//        );
//    }
//}