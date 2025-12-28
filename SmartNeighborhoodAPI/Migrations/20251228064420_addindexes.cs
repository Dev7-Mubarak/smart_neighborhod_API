using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialNeighborhood_AspNetUsers_NeighborhoodManagerId",
                table: "ResidentialNeighborhood");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhood_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResidentialNeighborhood",
                table: "ResidentialNeighborhood");

            migrationBuilder.RenameTable(
                name: "ResidentialNeighborhood",
                newName: "ResidentialNeighborhoods");

            migrationBuilder.RenameIndex(
                name: "IX_ResidentialNeighborhood_NeighborhoodManagerId",
                table: "ResidentialNeighborhoods",
                newName: "IX_ResidentialNeighborhoods_NeighborhoodManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentialUnits",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blocks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentialNeighborhoods",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResidentialNeighborhoods",
                table: "ResidentialNeighborhoods",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29e853df-296e-4117-b43a-84a5c4b265a9", "AQAAAAIAAYagAAAAEPAseORnKSJZ9S7QldA07dhf0pPsS62wtOHFruiTIq1eh5/chp8q7JHes7ygoqSNRQ==", "df765d19-ac7d-4479-9ade-09152e9592a7" });

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnits_Name",
                table: "ResidentialUnits",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_Name",
                table: "Blocks",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialNeighborhoods_Name",
                table: "ResidentialNeighborhoods",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialNeighborhoods_AspNetUsers_NeighborhoodManagerId",
                table: "ResidentialNeighborhoods",
                column: "NeighborhoodManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhoods_ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                column: "ResidentialNeighborhoodId",
                principalTable: "ResidentialNeighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialNeighborhoods_AspNetUsers_NeighborhoodManagerId",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhoods_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialUnits_Name",
                table: "ResidentialUnits");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_Name",
                table: "Blocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResidentialNeighborhoods",
                table: "ResidentialNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialNeighborhoods_Name",
                table: "ResidentialNeighborhoods");

            migrationBuilder.RenameTable(
                name: "ResidentialNeighborhoods",
                newName: "ResidentialNeighborhood");

            migrationBuilder.RenameIndex(
                name: "IX_ResidentialNeighborhoods_NeighborhoodManagerId",
                table: "ResidentialNeighborhood",
                newName: "IX_ResidentialNeighborhood_NeighborhoodManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentialUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentialNeighborhood",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResidentialNeighborhood",
                table: "ResidentialNeighborhood",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d6e6d59-96c4-4a28-a9a4-678c08103d41", "AQAAAAIAAYagAAAAEBLUSmWyvnp9FMo3HFx/k/O0OBtpZ73vc6Xjyn0mK+Bbc0TOkYYHF/BgpjCtBBL7ig==", "24395e7d-2078-4924-a630-c36a1d5a8c4b" });

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialNeighborhood_AspNetUsers_NeighborhoodManagerId",
                table: "ResidentialNeighborhood",
                column: "NeighborhoodManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhood_ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                column: "ResidentialNeighborhoodId",
                principalTable: "ResidentialNeighborhood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
