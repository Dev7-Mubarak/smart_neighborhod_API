using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class DropSyncColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CreatedAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Issues");

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
                values: new object[] { "8ad37b45-83e0-4ea2-b41a-897b27f284b5", "AQAAAAIAAYagAAAAEIdileAA8jBYZOyEejJEcCdMaF3q+99vMqt8Try70QGjrXPcJ/xk8Y42Uk9M+392Hw==", "ea65ba2b-bede-4f81-8f6c-167d610ee116" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c31bb9d-f1f3-4868-b9f1-6af408d686cd", "AQAAAAIAAYagAAAAEGDZe6OmFCuNQ/1ItEgVpsRwy+Ab7i5peE3q/ZSctPdec/DbCuUMFbLTrH2Hx583DA==", "30dc0fdd-10df-4b9b-a6fc-6403a936e681" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d35c510c-dde6-45e2-9953-07c162dbee29", "AQAAAAIAAYagAAAAEOeHyzrAKNb8eHRaaAK8ZLPDBm+De1qczXmYI/CYTStl3S3406hZxVNGj53LXRizWw==", "309136a0-6d25-40bc-9952-ae4aad233df3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e83391e-8ba7-414e-a7a2-247206283ce2", "AQAAAAIAAYagAAAAEOaGTsreufe7/NlURhL7S1XryOXRcUpeQjykSMYD9iCejYyexhE18VTVwA0zWLxYZw==", "61252185-44e1-4890-8a25-b6ded3f13fc0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CreatedAt",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Issues",
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
                values: new object[] { "edf5c63b-adb3-4ee2-aa29-14520897630b", "AQAAAAIAAYagAAAAEM3r4tLTvsopgFbrc6Bh5qLbisYVG7K31uVVKyyQ0rrqI32oQmZLmLRrk70miKZZEA==", "ff8c0970-1ac5-459d-bf71-6d28c34e2155" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3ab3acc-ff83-420f-82b8-32ef71cf15de", "AQAAAAIAAYagAAAAELTZVM8Hz1YaJEYOvlWSs4ERWbAZJ2AKss8N9toXn2ksk2pKA6JspToIxYlEye542g==", "8096bac0-be95-4a60-bff6-2c68539610df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89cb1568-f95b-4c28-96f0-b4a643e682ca", "AQAAAAIAAYagAAAAEHquJt5Vg45sprAhL/hc72GxXCLOM089xHJvz5V5uR0Y/KS+XKuxgW63cW0SJyJMZw==", "62b53589-2b2e-45b1-85cb-5c32f6e8b88b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d907e33c-16e1-41fc-999d-e70ed7b87e70", "AQAAAAIAAYagAAAAEINyP1bIcw4zyraYuWyXvYkpYp+xCuE8xnvtL/HOfPnAJEDWlSdDdJzQc6ylH1CEpw==", "b396c000-9ed5-43c2-961b-ab3177e49913" });
        }
    }
}
