using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddEmailOtpToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmationCode",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmationCodeExpiresAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "0dc627e1-9a00-4c2e-99ab-e76ca6ad4f98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "728e8489-5214-4b33-8234-d8d11b3b9a6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "ConcurrencyStamp",
                value: "1faebe33-40aa-4ba9-a29a-5e150f2561c5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmationCode", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6db8778-ba89-4924-9555-47f73daa9c92", "", "AQAAAAEAACcQAAAAECpZEyAaDFd7YJgCgwDhfxMD3xZQnsflRUcU9FfRgtdWpXKx/RAeu+VmbTZwuj9kDg==", "49438b32-a1dd-4b7f-a84a-7ccf78347858" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmationCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmationCodeExpiresAt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "ConcurrencyStamp",
                value: "a2abf634-ed00-4914-b277-c3d9a3bb73b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222",
                column: "ConcurrencyStamp",
                value: "38687b5a-bcc4-4e4a-8c23-1a37a2926e0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                column: "ConcurrencyStamp",
                value: "cc2e051c-bf34-431e-8e97-2dc35f20c488");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "650b3bb9-b04f-42d4-aaa8-183517943c88", "AQAAAAEAACcQAAAAED/COppCV7dQCORtRJn2b57J7K1E6VgJPgGnJI5ebzu7Up/evnRDdHRUWdhGbVmR6Q==", "ff506747-e928-4111-a9c1-81570cfabc24" });
        }
    }
}
