using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddComplainAndComplainTypeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4c70987-5bf5-448b-a68f-31cd1d688b72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c93e8a4b-6564-4edb-a3df-08ba6c495d39");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "27509c93-9113-4da3-b9cb-aedd1ba6e2ec", "4aa13875-0b23-4441-bd68-576a2862b22d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27509c93-9113-4da3-b9cb-aedd1ba6e2ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4aa13875-0b23-4441-bd68-576a2862b22d");

            migrationBuilder.CreateTable(
                name: "ComplainTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfilctParty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctParty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplainTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_ComplainTypes_ComplainTypeId",
                        column: x => x.ComplainTypeId,
                        principalTable: "ComplainTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonComplain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PesonId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ComplainId = table.Column<int>(type: "int", nullable: false),
                    ConfilctPartyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonComplain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonComplain_Complains_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "Complains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonComplain_ConfilctParty_ConfilctPartyId",
                        column: x => x.ConfilctPartyId,
                        principalTable: "ConfilctParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonComplain_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ComplainTypeId",
                table: "Complains",
                column: "ComplainTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_ComplainId",
                table: "PersonComplain",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_ConfilctPartyId",
                table: "PersonComplain",
                column: "ConfilctPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_PersonId",
                table: "PersonComplain",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonComplain");

            migrationBuilder.DropTable(
                name: "Complains");

            migrationBuilder.DropTable(
                name: "ConfilctParty");

            migrationBuilder.DropTable(
                name: "ComplainTypes");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27509c93-9113-4da3-b9cb-aedd1ba6e2ec", "8835bcac-7f78-403a-b186-3e59b18b3806", "Admin", "ADMIN" },
                    { "b4c70987-5bf5-448b-a68f-31cd1d688b72", "f75015dc-4575-4e9a-b477-54a5d2960f87", "User", "USER" },
                    { "c93e8a4b-6564-4edb-a3df-08ba6c495d39", "3624ee04-a9ed-4bc1-8887-558294d12af8", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4aa13875-0b23-4441-bd68-576a2862b22d", 0, "5c1706f7-485f-43cd-92d4-d59a8d621126", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEMkDAKOj5MNybTkdG544rx62//cpo4t/4DRsDT/BgNgC6Jni8OEV2jrl/KGqWhqVtg==", null, false, "9bb77d7b-5a5c-42e0-8a59-b3b6a89c9dda", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "27509c93-9113-4da3-b9cb-aedd1ba6e2ec", "4aa13875-0b23-4441-bd68-576a2862b22d" });
        }
    }
}
