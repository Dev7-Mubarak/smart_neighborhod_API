using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddProjectCatgroyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ProjectCatogories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCatogories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ProjectCatgoryId = table.Column<int>(type: "int", nullable: false),
                    ProjectCatogoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Procedures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: true),
                    Proiorty = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_ProjectCatogories_ProjectCatogoryId",
                        column: x => x.ProjectCatogoryId,
                        principalTable: "ProjectCatogories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemberJob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMember_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMember_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectCatogoryId",
                table: "Project",
                column: "ProjectCatogoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_ProjectId",
                table: "Team",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_PersonId",
                table: "TeamMember",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamId",
                table: "TeamMember",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ProjectCatogories");

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
        }
    }
}
