using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class selfjoininblocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_People_ManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Blocks",
                newName: "UnitManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Blocks_ManagerId",
                table: "Blocks",
                newName: "IX_Blocks_UnitManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ConfilctCases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "ConfilctCases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlockManagerId",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks",
                column: "BlockManagerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_UnitManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "BlockManagerId",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "UnitManagerId",
                table: "Blocks",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Blocks_UnitManagerId",
                table: "Blocks",
                newName: "IX_Blocks_ManagerId");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ConfilctCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "0a81216d-5342-4974-8f79-39edbeeda934", "admin@example.com", null, null, true, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEG1lXTnNOz040/Vv77tU7Phatd8lTszwJHFjQVPGrHJONmkrUsywdoaxGQ7nDu4YAA==", 1, null, false, "9fe6f59c-8931-44c7-8269-c94ad5fa0360", false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_People_ManagerId",
                table: "Projects",
                column: "ManagerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
