using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class DeleteTheImageByteFromComplainAndComplainDto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9850ca3-7a40-45c5-8cf4-b0a8a3859e92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd091a0b-0983-4ec4-b482-95fd4b5bf821");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8b70b607-1bc4-435a-9ac9-3ab45bcd7153", "263914e3-743b-4ebc-99a5-0a7d0c010a8b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b70b607-1bc4-435a-9ac9-3ab45bcd7153");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "263914e3-743b-4ebc-99a5-0a7d0c010a8b");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Complains");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Complains",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8b70b607-1bc4-435a-9ac9-3ab45bcd7153", "742083c9-c40a-483f-a744-2db0a829f050", "Admin", "ADMIN" },
                    { "b9850ca3-7a40-45c5-8cf4-b0a8a3859e92", "26481926-2e7d-403d-871a-0c0aff929de7", "User", "USER" },
                    { "cd091a0b-0983-4ec4-b482-95fd4b5bf821", "26dcc28a-e3e2-415e-bdde-2abf0780a26a", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "263914e3-743b-4ebc-99a5-0a7d0c010a8b", 0, "dbe6c577-261c-4cec-a7be-71d3e5d4e1d2", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEKuAkID+LwtkJN/IZvf6kkGuR5efefIxLWV7cF9pj2otaY8c0ecrcBnb/IjarB5oEQ==", null, false, "ffa1b3c5-a89e-453b-8f98-2458556e6e00", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8b70b607-1bc4-435a-9ac9-3ab45bcd7153", "263914e3-743b-4ebc-99a5-0a7d0c010a8b" });
        }
    }
}
