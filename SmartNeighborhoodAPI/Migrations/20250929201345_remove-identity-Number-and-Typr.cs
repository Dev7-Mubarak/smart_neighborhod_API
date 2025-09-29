using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeidentityNumberandTypr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IdentityType",
                table: "People");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bf7157a-3c1a-4982-bed1-1eebc022d1e2", "AQAAAAIAAYagAAAAEMsiJVkx8bWXS3OHs2DciIL+4fLaUgNSPR9LpvtE84cSV1onSAHfjoVOAjbkorSzhA==", "eab14988-eea3-4adf-b987-af76f1c31b66" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "People",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "People",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdentityType",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea1d2989-fd3d-416b-bd8a-71a8f4a6034e", "AQAAAAIAAYagAAAAEIWWeDUhtkBEGimS1lImTzpXl398ZcccWMtefd7FhPUM4CL7PkVtpcKNYu8agqO8OQ==", "c1515a72-e51d-44b7-9926-455f3dcc73af" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "IdentityNumber", "IdentityType" },
                values: new object[] { "ahmad@example.com", "894754369053", 1 });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "IdentityNumber", "IdentityType" },
                values: new object[] { "fatima@example.com", "8945784903588", 1 });
        }
    }
}
