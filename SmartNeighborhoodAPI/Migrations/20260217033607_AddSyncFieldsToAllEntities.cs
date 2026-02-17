using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSyncFieldsToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ResidentialUnits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ResidentialUnits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ResidentialUnits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResidentialUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ResidentialUnits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ResidentialNeighborhoods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ResidentialNeighborhoods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ResidentialNeighborhoods",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ResidentialNeighborhoods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ResidentialNeighborhoods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Projects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FamilyMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "FamilyMembers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FamilyMembers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FamilyMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ConfilctCases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ConfilctCases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ConfilctCases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ConfilctCases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ConfilctCases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Blocks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Blocks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Blocks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "736391b3-d174-464f-904e-fc972b221c01", "AQAAAAIAAYagAAAAEHCdl7+g1d4uuDUP8S6O4lE36MDkTJiOSFM22dUMrl5eqchBYJtXUzvzkcHwezl8gg==", "0b407601-d9fc-48d2-b20a-96187fd0a87e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9db32ab6-bb52-4728-98d2-98b2c92438e0", "AQAAAAIAAYagAAAAEGmZL7Dd1jxetDNoK2y2GsUDoVCl3DBqiPB1W2IgRhiYo5wZr+E60jt76JhEWaQH9A==", "8d93e605-57e5-4bad-a0fb-f9ee6800c969" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb5b7420-2a1f-4da7-942b-4f10d5a4b2bf", "AQAAAAIAAYagAAAAEPR8J0riO2YeNAUssPTU00bo/eYiyl2f+FegsrOP9q3yrZd2B/PciV0hYKNY3fS95w==", "6c951fda-dc05-4e36-89e4-8ebf821064b3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b242d81-b666-4b5f-ac9e-794a5c47b6f5", "AQAAAAIAAYagAAAAEBn80/dXffldeZzqN+HzcINcKRMzGTALLgyYVoG/jOMsPo3rXUD9CvUy7qM+YSN29A==", "a23a6d0f-ac87-4c20-80b8-95a3fb61aaa7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Blocks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f219730-2212-41d2-93a3-7b2247416062", "AQAAAAIAAYagAAAAEA3PNwYjKbu3XZYWh74otmjZ0H5U64NscjRQzcRBtFuBV46lqFNA3otg+A4SJ2vP4g==", "756b1956-3636-42da-bb25-9838a57feed6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3b2cf94-a672-42cb-a6c6-8624cf1e3497", "AQAAAAIAAYagAAAAEBCBbanmnpPiqyRjWZRhHT+6fnJ82EGd/ZE/GuZZlnmy/JEleRhTaWmswn6WwNXkaA==", "bfb41145-3207-420e-99d3-4cee58cd7ab7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0852f726-0235-490a-bc70-8de393c668ce", "AQAAAAIAAYagAAAAELPVYI88sX3GH8WnCwjOWiuaA+cmgyoKHif1Hy0cghzqCkEv5dOpBBiq5O7LvlxreQ==", "e14c2ca7-c405-4d10-bc72-45e55534827c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10055aee-40c8-419a-aec4-bfc5750f02ea", "AQAAAAIAAYagAAAAEKLsNCYShylnKoUSnX5syq2jAXfO1eqN7wG/XIkfI3lYjwMeBgV9jKa7+cSMTJ7YCw==", "ed3820a6-5b3d-486e-a1bb-e021da781736" });
        }
    }
}
