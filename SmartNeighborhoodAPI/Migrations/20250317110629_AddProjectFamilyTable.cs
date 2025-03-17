using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddProjectFamilyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ProjectFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFamilies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFamilies_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
  
                    table.ForeignKey(
                        name: "FK_ProjectFamilies_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "494089ff-5ac1-4814-ab52-745fcf2a68ee", "2274856c-39d7-44b9-bba2-531953cd6941", "BlockManager", "BLOCKMANAGER" },
                    { "9ab68f23-4709-44db-a3ec-946237e6f449", "9d1e9841-ad15-4f1b-b3ca-895e8df6b694", "User", "USER" },
                    { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "6a636be7-16b3-4936-9ba0-4bcc5fa89396", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "69bd8689-bc7d-49f8-9646-46aa525a7eb5", 0, "21561e88-1986-49c0-acb7-3ab0dadaa87c", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAENfvcsxqibuUtlWpZVnu//feu1MSzXlqwsQMsF0F55Wzu4WuahITRNnrpPZInnWUqA==", null, false, "484c45bd-4e5d-42f8-8079-205d97a9d863", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "69bd8689-bc7d-49f8-9646-46aa525a7eb5" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_BlockId",
                table: "ProjectFamilies",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_FamilyId",
                table: "ProjectFamilies",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_ProjectId",
                table: "ProjectFamilies",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectFamilies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "494089ff-5ac1-4814-ab52-745fcf2a68ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ab68f23-4709-44db-a3ec-946237e6f449");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "69bd8689-bc7d-49f8-9646-46aa525a7eb5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f17aeeef-ac3b-48f1-9d4f-0975ec11259f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69bd8689-bc7d-49f8-9646-46aa525a7eb5");

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
        }
    }
}
