using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

         

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad99dfa7-238d-43d5-8950-a9d9b20443ea", "AQAAAAIAAYagAAAAELvtVdpF8nmABz/4H5GqFUUcDXfca/2kG944piMMru/r934GQGMhwp8bp4SrLuTZ/A==", "4b832638-d017-40f3-9a8b-a274dc63d758" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "10101010-1010-1010-1010-101010101010", 0, "6574f636-5b87-4bd8-8e51-4ed6f2084929", "block.manager4@test.com", null, null, true, true, false, null, "BLOCK.MANAGER4@TEST.COM", "BLOCKMANAGER4", "AQAAAAIAAYagAAAAEFw+fPtV5q7PqHWHm2NJFDKuKW8RtgWBunDYvaoZNHQYL6GeJPZ1m7GQO+ypzg+R9A==", 7, null, false, "d7e7f941-2219-4c56-a843-ffac8ca8936f", false, "BlockManager4" },
                    { "20202020-2020-2020-2020-202020202020", 0, "dab6f815-bc3b-4be0-afe3-677d1b5db886", "block.manager5@test.com", null, null, true, true, false, null, "BLOCK.MANAGER5@TEST.COM", "BLOCKMANAGER5", "AQAAAAIAAYagAAAAEOPsReAutro/ww0+1r+BnLqjVm43YB7jlA4bDWCmZPaO8G3H42O/PCcqjMrFsDza6A==", 8, null, false, "327a2f3d-e86a-4219-9325-ebdb05e03971", false, "BlockManager5" },
                    { "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", 0, "5b8078e4-0766-46c3-a2bd-79d095a16916", "neighborhood.manager@test.com", null, null, true, true, false, null, "NEIGHBORHOOD.MANAGER@TEST.COM", "NEIGHBORHOODMANAGER1", "AQAAAAIAAYagAAAAECM+ebdtBxOdHf8Ik11L1BGPACJFNQMA/UTtBpRcErvb4ScdXSs4fE1j+SWVpDKm5w==", 2, null, false, "d562bbe3-dd64-4116-9e17-3a0a895ae2cb", false, "NeighborhoodManager1" },
                    { "cccccccc-cccc-cccc-cccc-cccccccccccc", 0, "cf12abfb-47a8-4975-84a2-1ae87f941846", "unit.manager@test.com", null, null, true, true, false, null, "UNIT.MANAGER@TEST.COM", "UNITMANAGER1", "AQAAAAIAAYagAAAAEB2EcEUWzhXDBAA5/2pmiI24rkOn5A/k+SZ9D2ZYmB+JldD+seI8P6fybmd4PSLZkg==", 3, null, false, "05703117-de1f-4de0-8f49-f7f0c61fa1f0", false, "UnitManager1" },
                    { "dddddddd-dddd-dddd-dddd-dddddddddddd", 0, "b2b618ef-9742-44f3-97e8-1f1631012b7f", "block.manager1@test.com", null, null, true, true, false, null, "BLOCK.MANAGER1@TEST.COM", "BLOCKMANAGER1", "AQAAAAIAAYagAAAAEGfUzijCeC1AkeF6dl/uZheFjjWHFMQBrw/3syF0h4Di1VowEG6JDBnk/OAJvs/4Pg==", 4, null, false, "bc62e86e-0b97-4666-a133-e39b58d3ba0e", false, "BlockManager1" },
                    { "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", 0, "a5b72a81-db98-4a99-acf9-bb37a23f0d45", "block.manager2@test.com", null, null, true, true, false, null, "BLOCK.MANAGER2@TEST.COM", "BLOCKMANAGER2", "AQAAAAIAAYagAAAAEHdVDV54QMwPESBHun19Oc5l4pvaPSsdAeWdEEGpyyeF1gCbDsDAnPBmtfzN29UszA==", 5, null, false, "7d4b5475-aeac-4106-bd26-f3643e5e2703", false, "BlockManager2" },
                    { "ffffffff-ffff-ffff-ffff-ffffffffffff", 0, "a9858722-f83e-4a12-a494-b8a9ee9c04ac", "block.manager3@test.com", null, null, true, true, false, null, "BLOCK.MANAGER3@TEST.COM", "BLOCKMANAGER3", "AQAAAAIAAYagAAAAEHLqeRjkyTHj2qYNdNNl4dA8X7CkmCyyRDCqaTlyC0N1NyqNtICQGhYk4GQyy/3kvQ==", 6, null, false, "cf5f6f53-52e2-4ad7-97c4-2bb71c1ded7b", false, "BlockManager3" }
                });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "personType",
                value: 3);

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BloodType", "DateOfBirth", "FirstName", "Gender", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "OccupationStatus", "PhoneNumber", "SecondName", "ThirdName", "personType" },
                values: new object[,]
                {
                    { 222, 0, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "أحمد", 0, null, true, true, null, "السعيد", 2, 2, "0501234567", "محمد", "علي", 0 },
                    { 223, 7, new DateTime(1988, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "خالد", 0, null, true, true, null, "العمري", 2, 3, "0502345678", "عبدالله", "محمد", 0 },
                    { 224, 2, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "محمد", 0, null, true, true, null, "الأحمدي", 1, 1, "0503456789", "سعيد", "أحمد", 0 },
                    { 225, 4, new DateTime(1987, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "عبدالرحمن", 0, null, true, true, null, "القحطاني", 2, 3, "0504567890", "فيصل", "عبدالله", 0 },
                    { 226, 0, new DateTime(1992, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "سعود", 0, null, true, true, null, "الدوسري", 1, 1, "0505678901", "يوسف", "علي", 0 },
                    { 227, 6, new DateTime(1989, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "فهد", 0, null, true, true, null, "الشمري", 2, 3, "0506789012", "إبراهيم", "محمد", 2 },
                    { 228, 2, new DateTime(1991, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ناصر", 0, null, true, true, null, "الغامدي", 1, 1, "0507890123", "علي", "سعد", 2 }
                });

            migrationBuilder.InsertData(
                table: "ResidentialNeighborhoods",
                columns: new[] { "Id", "Name", "NeighborhoodManagerId" },
                values: new object[] { 1, "حي السلام", "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb" });

            migrationBuilder.InsertData(
                table: "ResidentialUnits",
                columns: new[] { "Id", "Name", "ResidentialNeighborhoodId", "UnitManagerId" },
                values: new object[] { 1, "الوحدة الأولى", 1, "cccccccc-cccc-cccc-cccc-cccccccccccc" });

            migrationBuilder.InsertData(
                table: "Blocks",
                columns: new[] { "Id", "BlockManagerId", "Name", "ResidentialUnitId" },
                values: new object[,]
                {
                    { 1, "dddddddd-dddd-dddd-dddd-dddddddddddd", "المربع الأول", 1 },
                    { 2, "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee", "المربع الثاني", 1 },
                    { 3, "ffffffff-ffff-ffff-ffff-ffffffffffff", "المربع الثالث", 1 },
                    { 4, "10101010-1010-1010-1010-101010101010", "المربع الرابع", 1 },
                    { 5, "20202020-2020-2020-2020-202020202020", "المربع الخامس", 1 }
                });

  
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.DeleteData(
                table: "ResidentialUnits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc");

            migrationBuilder.DropColumn(
                name: "ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5bed72f-ebe0-4156-8af5-5f9b9d863239", "AQAAAAIAAYagAAAAEHxmSInlpDfnb+s5UUkggWAw9JKMBbBvKVoRCdVYRL3a4/l6OioQQVkHg9w9JJW/mw==", "f49b23a8-cb5e-43bd-85cc-cc99e9c62356" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "personType",
                value: 0);
        }
    }
}
