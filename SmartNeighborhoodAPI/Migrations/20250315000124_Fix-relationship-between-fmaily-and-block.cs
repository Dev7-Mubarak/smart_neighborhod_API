using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class Fixrelationshipbetweenfmailyandblock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6de5bfb-7c7a-4817-8f7f-14a258570487");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c166ba25-40a4-49d0-9fe7-459bb62c8458");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "48a90e84-ee44-4844-b748-824d85a4a383", "66b6699f-a4bc-45d4-af6e-6f1b5c363091" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48a90e84-ee44-4844-b748-824d85a4a383");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66b6699f-a4bc-45d4-af6e-6f1b5c363091");

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families");

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

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Families",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48a90e84-ee44-4844-b748-824d85a4a383", "f9258799-da1b-4968-ba18-5964d7b5fd49", "Admin", "ADMIN" },
                    { "a6de5bfb-7c7a-4817-8f7f-14a258570487", "ff15d392-72a3-4850-8049-08c65ee9678f", "User", "USER" },
                    { "c166ba25-40a4-49d0-9fe7-459bb62c8458", "898a60b6-6596-4b5b-bd77-65ae0315996a", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "66b6699f-a4bc-45d4-af6e-6f1b5c363091", 0, "427de4e8-0909-4f9e-8d41-9c50cb866194", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEKWl4GZ0hzGP8IKVW2PlvqiGzwhNSsiEpcPlm6oSZ8su1fTwDDvhfX9siJyyl//SJg==", null, false, "a1b73866-8bc4-4f0f-a251-4921d75ad2a0", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "48a90e84-ee44-4844-b748-824d85a4a383", "66b6699f-a4bc-45d4-af6e-6f1b5c363091" });

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Blocks_BlockId",
                table: "Families",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id");
        }
    }
}
