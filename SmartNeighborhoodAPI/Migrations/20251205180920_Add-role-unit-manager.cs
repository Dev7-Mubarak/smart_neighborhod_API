using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addroleunitmanager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "UnitManager", "UNITMANAGER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "e8e4bd9d-6dc4-4758-be89-f784dc0faaf1", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "sys.smartneighborhood@gmail.com", "ADMIN", "AQAAAAIAAYagAAAAENlQUdsy7Ddx44YO1NIqkN0ftSwdhtFTm2aJ4Apx6smVNyF5smMpm/dfihM33d8qcQ==", 9, null, false, "ea6efa2d-6b4e-4eb7-a4fc-8f30a9097bb9", false, "Admin" },
                    { "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa", 0, "c87c6b57-2155-4b60-b334-a435e0bd0218", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "SYS.SMARTNEIGHBORHOOD@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEA9rv6m9OcQcmEogNw2URe9Zp4tyIkERKToz3qfX8SR+vrBwXHzJpUW6WDECNyE2Pw==", 1, null, false, "0d7e9c09-304a-4d3b-9aa4-c240cd0ef31c", false, "Admin" }
                });
        }
    }
}
