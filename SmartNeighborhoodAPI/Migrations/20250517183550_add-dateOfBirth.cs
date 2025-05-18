using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class adddateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "People",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "b621a021-4916-43cc-abfb-e66064b0328f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "4913c3f1-d7e1-475f-b192-d61a3eac6866");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "ConcurrencyStamp",
                value: "9fcfca55-27d6-4437-9033-9b442f316b54");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aff3ea44-71eb-4f2a-97e0-e9f84a3e72f6", "AQAAAAEAACcQAAAAEC8YIag6dYSOv0iS1CLEd8I8OhnSCYtTcbd0OM4o0tF7YgMh7HlhSsElvvmP8XqreQ==", "470abd3f-c76d-4cee-9181-643f382fc73c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "People",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "5f637d8e-3659-47a4-ad53-65da89e4103f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "c3493947-b299-4ddf-808a-b5b18227fa76");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "ConcurrencyStamp",
                value: "d4459103-8858-4e80-964b-40d05cf667af");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66d88fb3-c9d5-4eed-84be-13546aa63e5a", "AQAAAAEAACcQAAAAEAbldH1PeuVVUCA8Rxd9ILmIBBIHcn85W+aYm9mmC5M/57nENJjCpPDs/tthjneR2Q==", "4e6933ca-9e75-4b69-9691-76df6d084459" });
        }
    }
}
