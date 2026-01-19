using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class seeddataforfamilies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb54d2f0-6595-4f93-8727-e5ab96e5fa20", "AQAAAAIAAYagAAAAELJDymwUOf4oAw0/wk/URXYhrHgTE173uIrdkG0PB/AgUx47aghzGXCl4NZ6A+XeKw==", "fbc82c86-d9cc-4f01-bfd4-5ffe9f6444a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2ea36b-696e-49d3-9a32-6890f3595b8f", "AQAAAAIAAYagAAAAEH3TDYhc6YI3QQ9HVJ0Z4J8bSUTv9qeAYSqrDSyrGx5JFJVH/3twookhqJaDEgicSw==", "57657e04-fb4d-4063-8be2-5c9f073a93fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21683088-4ec1-4ec5-9ea3-21fb77e7786d", "AQAAAAIAAYagAAAAEAy3JPVkFDvphF4YwdU9pYTDi1E2syl9obPzwkRsVdsnVAibhynU697BlfEZZRkb/Q==", "f79ae376-4d5a-4a85-8130-a3068e995613" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85000fb3-d7d6-47a5-8067-10ef46986a3f", "AQAAAAIAAYagAAAAEE1SvnaCaPFZpbQHFu5I6gvtqEvfhK5tDKir14YBqDVh/9Rl2tJ4yGm22rxZIgYqhA==", "0599cd83-7915-4434-bdeb-94c196cdd7e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "845c9793-6aa6-4c3d-8905-e965c2198a08", "AQAAAAIAAYagAAAAELdCOiyVIZttZ4Yl/Ck3fGrwgE9b/KSvdnFvIVfy0C+uZ980/5YCGb/GFZEnzZswgg==", "6f42c013-d1c5-429b-8307-4899707e9b89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a6e8858-48f1-4e11-a1cf-8f59a3e96e41", "AQAAAAIAAYagAAAAEBqG5JV2uaapkcXYtRZXgHBb+fRCG9va4IweC2x/30pa1ZQsvqtf1sgOOCJHVFahhQ==", "e8c0ee21-1707-4714-8d6f-3b70e8559d33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2276b22-238d-41dc-8093-7833b547b3b9", "AQAAAAIAAYagAAAAEP0ItioypvBtT6icR2PNTB5m6PZCKydydokQRqVtsLTMiTzQ7JaI9PRiCFmpkWba+g==", "6f960618-9dc9-49f9-823e-af7fb8664c87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3790f01f-2294-4cc4-8bc1-2cba31daa7de", "AQAAAAIAAYagAAAAEGzdhEPSmtiMtoJWd4LDDJlViAc5ZGjep9dwaSIxzDgWstp3HhjQSmDmqzEhM94VlA==", "9452030b-8d9a-476b-9104-61f13fb6b2c5" });

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "BlockId", "FamilyCatgoryId", "FamilyNotes", "HousingType", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 1, 1, "عائلة محتاجة للدعم", 2, "شارع الملك فهد، حي السلام", "عائلة الأحمدي" },
                    { 2, 1, 3, "عائلة متوسطة الدخل", 1, "شارع العروبة، حي السلام", "عائلة السعيد" },
                    { 3, 2, 2, "عائلة يتيمة", 2, "شارع الأمير سلطان، حي السلام", "عائلة القحطاني" },
                    { 4, 2, 3, null, 1, "شارع الملك عبدالعزيز، حي السلام", "عائلة الدوسري" },
                    { 5, 3, 1, "عائلة كبيرة", 2, "شارع الخليج، حي السلام", "عائلة الشمري" },
                    { 6, 3, 3, null, 1, "شارع الأمل، حي السلام", "عائلة الغامدي" },
                    { 7, 4, 2, "عائلة محتاجة", 2, "شارع النخيل، حي السلام", "عائلة العمري" },
                    { 8, 4, 3, null, 1, "شارع الورود، حي السلام", "عائلة المطيري" },
                    { 9, 5, 1, "عائلة أرملة", 2, "شارع السلام، حي السلام", "عائلة الزهراني" },
                    { 10, 5, 3, null, 1, "شارع الفيحاء، حي السلام", "عائلة الحربي" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Families",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6574f636-5b87-4bd8-8e51-4ed6f2084929", "AQAAAAIAAYagAAAAEFw+fPtV5q7PqHWHm2NJFDKuKW8RtgWBunDYvaoZNHQYL6GeJPZ1m7GQO+ypzg+R9A==", "d7e7f941-2219-4c56-a843-ffac8ca8936f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dab6f815-bc3b-4be0-afe3-677d1b5db886", "AQAAAAIAAYagAAAAEOPsReAutro/ww0+1r+BnLqjVm43YB7jlA4bDWCmZPaO8G3H42O/PCcqjMrFsDza6A==", "327a2f3d-e86a-4219-9325-ebdb05e03971" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad99dfa7-238d-43d5-8950-a9d9b20443ea", "AQAAAAIAAYagAAAAELvtVdpF8nmABz/4H5GqFUUcDXfca/2kG944piMMru/r934GQGMhwp8bp4SrLuTZ/A==", "4b832638-d017-40f3-9a8b-a274dc63d758" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b8078e4-0766-46c3-a2bd-79d095a16916", "AQAAAAIAAYagAAAAECM+ebdtBxOdHf8Ik11L1BGPACJFNQMA/UTtBpRcErvb4ScdXSs4fE1j+SWVpDKm5w==", "d562bbe3-dd64-4116-9e17-3a0a895ae2cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf12abfb-47a8-4975-84a2-1ae87f941846", "AQAAAAIAAYagAAAAEB2EcEUWzhXDBAA5/2pmiI24rkOn5A/k+SZ9D2ZYmB+JldD+seI8P6fybmd4PSLZkg==", "05703117-de1f-4de0-8f49-f7f0c61fa1f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2b618ef-9742-44f3-97e8-1f1631012b7f", "AQAAAAIAAYagAAAAEGfUzijCeC1AkeF6dl/uZheFjjWHFMQBrw/3syF0h4Di1VowEG6JDBnk/OAJvs/4Pg==", "bc62e86e-0b97-4666-a133-e39b58d3ba0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5b72a81-db98-4a99-acf9-bb37a23f0d45", "AQAAAAIAAYagAAAAEHdVDV54QMwPESBHun19Oc5l4pvaPSsdAeWdEEGpyyeF1gCbDsDAnPBmtfzN29UszA==", "7d4b5475-aeac-4106-bd26-f3643e5e2703" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9858722-f83e-4a12-a494-b8a9ee9c04ac", "AQAAAAIAAYagAAAAEHLqeRjkyTHj2qYNdNNl4dA8X7CkmCyyRDCqaTlyC0N1NyqNtICQGhYk4GQyy/3kvQ==", "cf5f6f53-52e2-4ad7-97c4-2bb71c1ded7b" });
        }
    }
}
