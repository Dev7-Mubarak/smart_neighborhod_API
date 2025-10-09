using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockAndBlockIdToConfilctCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7979c66-b7e0-44dd-a910-fb7890d734a3", "AQAAAAIAAYagAAAAEP9WutKZAWlzse6NlI6qbQfPAaazYJY3Vfi9+9JEDu4rTqDNI3S4okXnjZBQrO55tQ==", "b845746f-1b59-41ed-bf0a-7975dcfbd2f1" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases",
                column: "BlockId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "ConfilctCases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a81216d-5342-4974-8f79-39edbeeda934", "AQAAAAIAAYagAAAAEG1lXTnNOz040/Vv77tU7Phatd8lTszwJHFjQVPGrHJONmkrUsywdoaxGQ7nDu4YAA==", "9fe6f59c-8931-44c7-8269-c94ad5fa0360" });
        }
    }
}
