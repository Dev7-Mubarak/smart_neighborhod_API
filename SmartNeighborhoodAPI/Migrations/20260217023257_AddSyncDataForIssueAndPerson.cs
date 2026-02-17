using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSyncDataForIssueAndPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Teams",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConflictResolution",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyncStatus",
                table: "Teams",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "People",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Issues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Issues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Families",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Families",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Families",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Families",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ConflictResolution",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "SyncStatus",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "People");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "People");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Families");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c89bfaf-211c-4009-a8cb-cd7b75dd7feb", "AQAAAAIAAYagAAAAENZX7hRXfko+fcAU1jFRDT8za2PKfJ5k//niB70kx8Ny332SUmd/44jMuMRg4faOGQ==", "8e86b6a4-6a1f-4d5a-b10f-38a3fcb23c86" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb81d958-7f5e-4ab6-a593-fb1528a467f3", "AQAAAAIAAYagAAAAENDqW8LpwPqhicMTW1svWF1ek9uF03rYzh6nsaHRmVWjVGICRm4zxZrsjvDIi0mX8Q==", "61f596a8-4aef-404d-9699-f348a4a5aadd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "092d80b8-a1ec-4cb7-9fb8-e1c685d83f28", "AQAAAAIAAYagAAAAEM6IEJIBfafCzVxEhQy+Sh3wqz6SDo0sttxy5Ur574o578I1Tl/uuEHqVAiBZhhnkA==", "ae25c687-bc80-4cd4-9a64-36673a0a2124" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "149dd9bf-bf95-4591-b6a0-5529609b45db", "AQAAAAIAAYagAAAAEP1TO81k/kSkSmHc/1rTaMGLhFIaB9xrEvnl/plNPbqGcBnbwPBnV7su83x+kzjQvA==", "d4160704-30aa-46c2-a155-bc7d5726667a" });
        }
    }
}
