using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa", 0, "827e78f4-64fb-4988-93d7-2e827fec53a8", "sys.smartneighborhood@gmail.com", null, null, true, false, false, null, "SYS.SMARTNEIGHBORHOOD@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMNFnV481FAfASxgMjPtK9vjqvVIVkHk5AHcifE81x7n86lZ11xwEX/VYYTzuF4ZoQ==", 9, null, false, "cfe93dd9-0b84-4611-bfc3-364ec2f1e97a", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa");
        }
    }
}
