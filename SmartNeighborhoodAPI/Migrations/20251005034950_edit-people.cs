using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class editpeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsWhatsapp",
                table: "People",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsContactNumber",
                table: "People",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a81216d-5342-4974-8f79-39edbeeda934", "AQAAAAIAAYagAAAAEG1lXTnNOz040/Vv77tU7Phatd8lTszwJHFjQVPGrHJONmkrUsywdoaxGQ7nDu4YAA==", "9fe6f59c-8931-44c7-8269-c94ad5fa0360" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsContactNumber", "IsWhatsapp" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsContactNumber", "IsWhatsapp" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsWhatsapp",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsContactNumber",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "faa06007-82b7-4a01-91c1-11004344aa5f", "AQAAAAIAAYagAAAAEE6E60hGHYnksHnlQvmaOvlsgvDrX+E/I47pq1ABDcNr/xzkxew/ULlSR1PfnfBR6g==", "5459f40e-d2f3-4f91-aaa6-216f1022bcd6" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsContactNumber", "IsWhatsapp" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsContactNumber", "IsWhatsapp" },
                values: new object[] { false, false });
        }
    }
}
