using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeManagerIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_People_ManagerId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "ConfilctCases",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId1",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0418dca3-0106-49dc-af9d-eb175389caf8", "AQAAAAIAAYagAAAAELQ3vVW8LJeJDstW4za7K+SENPfSb6sRyeWWaPCWpdU/1qCoBOD/1195SCqg/cN/KQ==", "ec6065b0-e396-485b-baf1-304abb4e0a6f" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ManagerId1",
                table: "ConfilctCases",
                column: "ManagerId1");

        
       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_People_ManagerId1",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_ManagerId1",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "ManagerId1",
                table: "ConfilctCases");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "275fc87a-d8a3-4640-a795-3f0e8eb99c62", "AQAAAAIAAYagAAAAECtD13HDpQgqjTSDHzwfJ1//Y5fo7+VHKcsr8W1+ybrcGsH5Xux2o2xHzX57/pqW1g==", "e05dba57-e5aa-47db-8626-a609e5aa5cfe" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_People_ManagerId",
                table: "ConfilctCases",
                column: "ManagerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
