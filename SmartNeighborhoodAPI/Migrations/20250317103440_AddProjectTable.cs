using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectCatogories_ProjectCatogoryId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Project_ProjectId",
                table: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0001f0e6-ec2a-4020-b9fb-1639041b30b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b66aadd0-49b3-48b1-9663-5e9fd27eff57");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21e3bc0b-4ce5-4af6-b6f4-4e7c4e327f9f", "efe7b959-7194-42bf-ad82-f82c5f0eeec4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21e3bc0b-4ce5-4af6-b6f4-4e7c4e327f9f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efe7b959-7194-42bf-ad82-f82c5f0eeec4");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Project_ProjectCatogoryId",
                table: "Projects",
                newName: "IX_Projects_ProjectCatogoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "181152d5-459f-4ec8-b19b-b1e315de9c1b", "42ce7a25-e25f-45cd-a58e-3cc0abecbe1a", "Admin", "ADMIN" },
                    { "92ec229d-d570-4035-a3dc-96e4cc6e0377", "ce7056d6-0c7b-4094-b570-c6fad002e524", "User", "USER" },
                    { "ae7be26a-c8aa-4787-91ea-58ac6a0c7aa4", "a4c37d24-1520-4748-8b4d-71aed27a3047", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fd4e0694-5f45-4191-8e55-e14531e991ac", 0, "e3b4f76e-e1b4-499a-8479-114cc60db0d3", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEDGMqwHgj4I9uCwbp9BemWXli37vpX9nAtZqiS2REhG/x6AQkMFRd/+MoVjLCik8Cw==", null, false, "45c0e1bd-55e2-4c43-8193-4ecb62de0365", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "181152d5-459f-4ec8-b19b-b1e315de9c1b", "fd4e0694-5f45-4191-8e55-e14531e991ac" });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectCatogories_ProjectCatogoryId",
                table: "Projects",
                column: "ProjectCatogoryId",
                principalTable: "ProjectCatogories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Projects_ProjectId",
                table: "Team",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectCatogories_ProjectCatogoryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Projects_ProjectId",
                table: "Team");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92ec229d-d570-4035-a3dc-96e4cc6e0377");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae7be26a-c8aa-4787-91ea-58ac6a0c7aa4");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "181152d5-459f-4ec8-b19b-b1e315de9c1b", "fd4e0694-5f45-4191-8e55-e14531e991ac" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "181152d5-459f-4ec8-b19b-b1e315de9c1b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd4e0694-5f45-4191-8e55-e14531e991ac");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ProjectCatogoryId",
                table: "Project",
                newName: "IX_Project_ProjectCatogoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0001f0e6-ec2a-4020-b9fb-1639041b30b7", "c662bf09-4295-4550-b729-82a085661417", "User", "USER" },
                    { "21e3bc0b-4ce5-4af6-b6f4-4e7c4e327f9f", "7ad684f3-8220-4104-af12-9bce520de49e", "Admin", "ADMIN" },
                    { "b66aadd0-49b3-48b1-9663-5e9fd27eff57", "cd5d6dab-e3eb-4880-9496-64c93a2d9cf2", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "efe7b959-7194-42bf-ad82-f82c5f0eeec4", 0, "643e6efe-ae6a-481d-9f89-577da8bc326b", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEKjwDHgSEG5pZjlYdaK2FY/RN4DRwGDRj0qhJkvX9eLSB2O7p1bRj0/mqAgX0nNxRw==", null, false, "f9255631-35cd-4ae9-9dbd-29582341deef", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "21e3bc0b-4ce5-4af6-b6f4-4e7c4e327f9f", "efe7b959-7194-42bf-ad82-f82c5f0eeec4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectCatogories_ProjectCatogoryId",
                table: "Project",
                column: "ProjectCatogoryId",
                principalTable: "ProjectCatogories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Project_ProjectId",
                table: "Team",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
