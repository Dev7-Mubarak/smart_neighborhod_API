using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class seeduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5403d1c2-fad2-403c-b5c7-2d2fc039c01b", 0, "4019558c-4384-4c63-bc91-444f91716eb0", "admin@example.com", true, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAELHpzWHrSkZPFTjXvnUkrIlm5QGt+vSKtq9O0rrriu5OuaSuatpVrTC+IFiSo6AioA==", 2, null, false, "a6117bfa-97e6-4f30-80cc-8909d8129895", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d2db89ef-c3e5-419a-9034-4b8fc930e0aa", "5403d1c2-fad2-403c-b5c7-2d2fc039c01b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "169c5d30-1cd6-4c86-a584-489d7f88ea9c", 0, "138d98e3-4ec3-4658-b5f7-6336e838c770", "admin@example.com", true, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEDj66ARA/h8C9FVik0KunP/2oYC+O1k6+BEPdjrQpzZXbDaWx7FjUGtGopXXp7dNjA==", 0, null, false, "198fbf39-f122-4ca1-938b-500bbb2fbfcc", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "be7da0e0-7602-41e4-b609-39725259e054", "169c5d30-1cd6-4c86-a584-489d7f88ea9c" });
        }
    }
}
