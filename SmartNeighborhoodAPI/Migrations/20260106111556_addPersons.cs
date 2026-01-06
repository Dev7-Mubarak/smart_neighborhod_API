using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


   



            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87cd3abb-7704-4160-a5c0-e6e2ca91edca", "AQAAAAIAAYagAAAAEBO06z2LLg//WIaIjXwgoWMZnmKyhpXjGtqx5tE7MKAGGuzzwAwVKr/ECXK+LW8SnA==", "67bbd22b-b4e3-4dd8-830d-b55021d1d39f" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BloodType", "DateOfBirth", "FirstName", "Gender", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "OccupationStatus", "PhoneNumber", "SecondName", "ThirdName", "personType" },
                values: new object[,]
                {
                    { 9, 5, null, "أحمد", 0, null, null, null, "مهندس", "الزهيري", 2, 2, "0791234567", "سعيد", "محمود", 3 },
                    { 10, 5, null, "فاطمة", 1, null, null, null, "طالبة", "الحسني", 1, 1, "0789876543", "خالد", "عبد الله", 0 },
                    { 11, 6, null, "محمد", 0, null, null, null, "محاسب", "الكعبي", 2, 2, "0771122334", "علي", "يوسف", 0 },
                    { 12, 0, null, "ريم", 1, null, null, null, "ربة منزل", "الشامسي", 2, 4, "0799988776", "حسن", "سالم", 0 },
                    { 13, 3, null, "سالم", 0, null, null, null, "عاطل عن العمل", "المري", 1, 3, "0785544332", "ناصر", "محمد", 0 },
                    { 14, 7, null, "نورة", 1, null, null, null, "طالبة جامعية", "العنزي", 1, 1, "0777766554", "عبدالرحمن", "خليل", 0 },
                    { 15, 4, null, "خالد", 0, null, null, null, "مدير مبيعات", "الهاشمي", 3, 2, "0796655443", "ابراهيم", "مصطفى", 0 },
                    { 16, 2, null, "زينب", 1, null, null, null, "مدرسة", "الخطيب", 4, 2, "0782233445", "محمود", "فارس", 0 },
                    { 17, 1, null, "عمر", 0, null, null, null, "تاجر", "السويدي", 2, 5, "0773344556", "فهد", "جابر", 0 },
                    { 18, 6, null, "منى", 1, null, null, null, "طالبة مدرسة", "المنصوري", 1, 1, "0794455667", "ياسر", "سلطان", 0 }
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhoods_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DropTable(
                name: "ResidentialNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialUnits_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DropColumn(
                name: "ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5bed72f-ebe0-4156-8af5-5f9b9d863239", "AQAAAAIAAYagAAAAEHxmSInlpDfnb+s5UUkggWAw9JKMBbBvKVoRCdVYRL3a4/l6OioQQVkHg9w9JJW/mw==", "f49b23a8-cb5e-43bd-85cc-cc99e9c62356" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "BloodType", "DateOfBirth", "FirstName", "Gender", "Image", "IsContactNumber", "IsWhatsapp", "Job", "LastName", "MaritalStatus", "OccupationStatus", "PhoneNumber", "SecondName", "ThirdName", "personType" },
                values: new object[,]
                {
                    { 1, 5, null, "أحمد", 0, null, null, null, "مهندس", "الزهيري", 2, 2, "0791234567", "سعيد", "محمود", 0 },
                    { 2, 5, null, "فاطمة", 1, null, null, null, "طالبة", "الحسني", 1, 1, "0789876543", "خالد", "عبد الله", 0 }
                });
        }
    }
}
