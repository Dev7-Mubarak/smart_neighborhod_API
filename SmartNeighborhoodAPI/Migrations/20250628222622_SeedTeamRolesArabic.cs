using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeamRolesArabic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "165e8ffa-cccc-4ed8-92b7-94b2dc1ea589", "AQAAAAIAAYagAAAAEMD5L5LkRdI9vB+zVujIFMnS/vB4jWLdciUz0RSaQGKkcpmAmdXI+hEOsRxd4qneYA==", "96e9d731-24c0-469c-a11e-351382e592f9" });

            migrationBuilder.InsertData(
                table: "TeamRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "مدير المشروع" },
                    { 2, "النائب" },
                    { 3, "عضو" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TeamRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeamRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TeamRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a247399f-9e02-4834-8441-4765c327379a", "AQAAAAIAAYagAAAAEE9pHUAGvd8ZmadbsbrDOfPrR5xqW9sk5exLiJpiCVdI54Rsdt/RoYx9dyplqfhREg==", "0ca97a27-13d6-4ade-b87a-7ee807492f74" });
        }
    }
}
