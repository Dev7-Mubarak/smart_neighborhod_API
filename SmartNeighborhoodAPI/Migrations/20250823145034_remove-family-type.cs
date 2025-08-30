using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class removefamilytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_FamilyTypes_FamilyTypeId",
                table: "Families");

            migrationBuilder.DropTable(
                name: "FamilyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Families_FamilyTypeId",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "FamilyTypeId",
                table: "Families");

            migrationBuilder.AlterColumn<string>(
                name: "FamilyNotes",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea1d2989-fd3d-416b-bd8a-71a8f4a6034e", "AQAAAAIAAYagAAAAEIWWeDUhtkBEGimS1lImTzpXl398ZcccWMtefd7FhPUM4CL7PkVtpcKNYu8agqO8OQ==", "c1515a72-e51d-44b7-9926-455f3dcc73af" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FamilyNotes",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyTypeId",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FamilyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c789bf98-ed08-406d-af2e-2f43ea1fd3c3", "AQAAAAIAAYagAAAAELuWC/mj29YxdmULlhd4ZD+dpL0vninw8evRSkHZ7oJllbGTOKmq3t0JKFOktgS+lw==", "a946d8db-e79d-46eb-886c-e0b077b40380" });

            migrationBuilder.InsertData(
                table: "FamilyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "عائلة أيتام" },
                    { 2, "عائلة أرامل" },
                    { 3, "عائلة طبيعية" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Families_FamilyTypeId",
                table: "Families",
                column: "FamilyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_FamilyTypes_FamilyTypeId",
                table: "Families",
                column: "FamilyTypeId",
                principalTable: "FamilyTypes",
                principalColumn: "Id");
        }
    }
}
