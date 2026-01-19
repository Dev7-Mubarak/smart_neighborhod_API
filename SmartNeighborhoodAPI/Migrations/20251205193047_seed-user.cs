using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class seeduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "c5bed72f-ebe0-4156-8af5-5f9b9d863239", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "sys.smartneighborhood@gmail.com", "ADMIN", "AQAAAAIAAYagAAAAEHxmSInlpDfnb+s5UUkggWAw9JKMBbBvKVoRCdVYRL3a4/l6OioQQVkHg9w9JJW/mw==", 1, null, false, "f49b23a8-cb5e-43bd-85cc-cc99e9c62356", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        }
    }
}
