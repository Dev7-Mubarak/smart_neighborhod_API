using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdddateReceivedtoProjectFamily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateReceived",
                table: "ProjectFamilies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c4c3d08-b706-48fe-9cfa-ea69c87a65c7", "AQAAAAIAAYagAAAAEEelOrtut6rMF5dDIfCELHkB7Jz4Uw6FvlIJC9Vq4XDshpVwL0ALLpG/UeikEeXyPQ==", "f8294112-0494-4952-bb2e-d38292c3782f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateReceived",
                table: "ProjectFamilies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "165e8ffa-cccc-4ed8-92b7-94b2dc1ea589", "AQAAAAIAAYagAAAAEMD5L5LkRdI9vB+zVujIFMnS/vB4jWLdciUz0RSaQGKkcpmAmdXI+hEOsRxd4qneYA==", "96e9d731-24c0-469c-a11e-351382e592f9" });
        }
    }
}
