using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class selfjoinInBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks");

            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Blocks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "BlockManagerId",
                table: "Blocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitManagerId",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e37e176-9e6f-418f-8319-49f62303daa7", "AQAAAAIAAYagAAAAEAmbVGpubDkryyF8i7rPaZHEeJvFObtkjFTC6EZIFM9xWcHaoLugo1OaULvK+ErkIA==", "94ac9aec-73b8-4ea3-8533-4ad0ac3282c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "023cc77a-a704-4337-be52-2cd3fd263d28", "AQAAAAIAAYagAAAAEPMjuVYj4pTZxvvXpLT3vjPAxpeYpRaQ7NaNFdO7g3oWIDAHt9Mfrwfl0mGOLyFdZg==", "17c52069-5b00-4c55-94cb-8e557a61a374" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "BlockId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BlockId",
                table: "Projects",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Blocks_BlockId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_BlockId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "UnitManagerId",
                table: "Blocks");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Blocks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93d414b2-2477-4865-b63f-079cc5667102", "AQAAAAIAAYagAAAAEEcbxv0gU49pdaP6+AFL1mD2h1xndEKfde7nCcpGvsu8VM1MBCYOjusUPhSgo4iP9Q==", "925ccef5-7127-4391-8374-250fff59e9d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2f83755-0ff0-475e-b40d-581d68d05535", "AQAAAAIAAYagAAAAEOLbob1ZcQrhaad2lSLaWMtaSlSvqZk0GZ+fbCMnlUEmlDupfRSPayzd5gDM2osp/Q==", "a1f06bba-5f1a-4ca1-958a-51f612b8c162" });

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
