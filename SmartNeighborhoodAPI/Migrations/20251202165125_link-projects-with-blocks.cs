using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class linkprojectswithblocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BlockId",
                table: "Projects",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Blocks_BlockId",
                table: "Projects",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Blocks_BlockId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_BlockId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Projects");
        }
    }
}
