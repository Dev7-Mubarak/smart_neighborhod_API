using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddGroupAndAdsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3be9ef7f-a970-4312-ae58-054e5c190b19");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6127f289-06fe-42b5-b660-12fa08c722c0");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e4b284eb-4929-4f02-93a5-cad497df3c82", "ee62ca4d-cc78-4d6a-9dac-f876b2e2b685" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4b284eb-4929-4f02-93a5-cad497df3c82");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ee62ca4d-cc78-4d6a-9dac-f876b2e2b685");

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdsText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdGroup",
                columns: table => new
                {
                    AdsId = table.Column<int>(type: "int", nullable: false),
                    GroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdGroup", x => new { x.AdsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_AdGroup_Ads_AdsId",
                        column: x => x.AdsId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AdGroup_GroupsId",
                table: "AdGroup",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdGroup");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Groups");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3be9ef7f-a970-4312-ae58-054e5c190b19", "7ded7c67-44e6-448c-8186-8505351e994f", "BlockManager", "BLOCKMANAGER" },
                    { "6127f289-06fe-42b5-b660-12fa08c722c0", "3575bd20-bcc2-478e-8d68-1ad8f4f6f64e", "User", "USER" },
                    { "e4b284eb-4929-4f02-93a5-cad497df3c82", "0161f74a-6a93-4ec5-a175-8ee461f92dd6", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ee62ca4d-cc78-4d6a-9dac-f876b2e2b685", 0, "a65e2144-9a36-4755-938a-eca689832f89", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEIsPcbfvKpiSR85ZzNcCVwt85O3+F8ISOfZFyeGYQmhix2wbvYC37AB/b3S88UpVcQ==", null, false, "8ac26555-8c10-4986-b977-7ae616007e97", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e4b284eb-4929-4f02-93a5-cad497df3c82", "ee62ca4d-cc78-4d6a-9dac-f876b2e2b685" });
        }
    }
}
