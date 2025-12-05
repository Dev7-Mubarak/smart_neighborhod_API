using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitAndBlockManagementRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_UnitManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_UnitManagerId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "UnitManagerId",
                table: "Blocks");

            migrationBuilder.AlterColumn<string>(
                name: "BlockManagerId",
                table: "Blocks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ResidentialUnitId",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResidentialUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialUnit_AspNetUsers_UnitManagerId",
                        column: x => x.UnitManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ResidentialUnitId",
                table: "Blocks",
                column: "ResidentialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnit_UnitManagerId",
                table: "ResidentialUnit",
                column: "UnitManagerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_ResidentialUnit_ResidentialUnitId",
                table: "Blocks",
                column: "ResidentialUnitId",
                principalTable: "ResidentialUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_ResidentialUnit_ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.DropTable(
                name: "ResidentialUnit");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.AlterColumn<int>(
                name: "BlockManagerId",
                table: "Blocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UnitManagerId",
                table: "Blocks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_UnitManagerId",
                table: "Blocks",
                column: "UnitManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_UnitManagerId",
                table: "Blocks",
                column: "UnitManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
