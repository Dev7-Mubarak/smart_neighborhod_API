using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddConfilctPartyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonComplain_ConfilctParty_ConfilctPartyId",
                table: "PersonComplain");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfilctParty",
                table: "ConfilctParty");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0513b041-a265-4aeb-abd9-c911e41e0434");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7974f5fe-0761-40f5-a64a-0299c824ab4d");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2bcb21c8-881d-457d-ac8d-29128da560cd", "b6939398-f671-4a1f-a1b8-7af67fda781c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bcb21c8-881d-457d-ac8d-29128da560cd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b6939398-f671-4a1f-a1b8-7af67fda781c");

            migrationBuilder.RenameTable(
                name: "ConfilctParty",
                newName: "ConfilctParties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfilctParties",
                table: "ConfilctParties",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0437f4ec-f456-4ac7-81b9-aa15ad624a4e", "f07fbca0-0f30-4b28-8f45-4328c22bd43c", "Admin", "ADMIN" },
                    { "58a92990-78d7-45f9-922b-3e146c33c8da", "63351ae1-0d73-4e53-bdee-a9c5c3cbd5e6", "BlockManager", "BLOCKMANAGER" },
                    { "f7eb717e-fca5-450b-8edd-6d9b25680790", "c6640619-6c34-403c-aff3-e8da077aa7be", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8376a0ca-69cd-4821-8f65-78707086b070", 0, "df0acc34-7c0a-46f1-b492-328cb5742d39", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAED3mbPRKXDvWED4aO06uTBlKteCw4qLjEAevq4cezQPVXEGID3P7bsodLK6KSfaWAA==", null, false, "52b7c234-8cae-41f1-8479-2d9482ff2884", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0437f4ec-f456-4ac7-81b9-aa15ad624a4e", "8376a0ca-69cd-4821-8f65-78707086b070" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonComplain_ConfilctParties_ConfilctPartyId",
                table: "PersonComplain",
                column: "ConfilctPartyId",
                principalTable: "ConfilctParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonComplain_ConfilctParties_ConfilctPartyId",
                table: "PersonComplain");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfilctParties",
                table: "ConfilctParties");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58a92990-78d7-45f9-922b-3e146c33c8da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7eb717e-fca5-450b-8edd-6d9b25680790");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0437f4ec-f456-4ac7-81b9-aa15ad624a4e", "8376a0ca-69cd-4821-8f65-78707086b070" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0437f4ec-f456-4ac7-81b9-aa15ad624a4e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8376a0ca-69cd-4821-8f65-78707086b070");

            migrationBuilder.RenameTable(
                name: "ConfilctParties",
                newName: "ConfilctParty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfilctParty",
                table: "ConfilctParty",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0513b041-a265-4aeb-abd9-c911e41e0434", "49c016bf-c59f-4e0f-8e13-d0699a1772cd", "User", "USER" },
                    { "2bcb21c8-881d-457d-ac8d-29128da560cd", "a4a64845-14c7-4741-b5d9-8bb5c2d7975d", "Admin", "ADMIN" },
                    { "7974f5fe-0761-40f5-a64a-0299c824ab4d", "3b88534b-c9cd-46ba-9572-8b485af0e51d", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b6939398-f671-4a1f-a1b8-7af67fda781c", 0, "5259e263-578c-48e5-ab6e-cf80189f6cba", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEDmTMOhEgBFBdUQHezUkjZBZn370tnnq6xJsLnHIZU1Pa2Xcmk556K6TYSlgy22xbQ==", null, false, "8ea9343f-87d0-4993-a117-ae4314c9c8b5", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2bcb21c8-881d-457d-ac8d-29128da560cd", "b6939398-f671-4a1f-a1b8-7af67fda781c" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonComplain_ConfilctParty_ConfilctPartyId",
                table: "PersonComplain",
                column: "ConfilctPartyId",
                principalTable: "ConfilctParty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
