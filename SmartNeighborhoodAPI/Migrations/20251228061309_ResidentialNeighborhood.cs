using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class ResidentialNeighborhood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResidentialNeighborhood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeighborhoodManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialNeighborhood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialNeighborhood_AspNetUsers_NeighborhoodManagerId",
                        column: x => x.NeighborhoodManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d6e6d59-96c4-4a28-a9a4-678c08103d41", "AQAAAAIAAYagAAAAEBLUSmWyvnp9FMo3HFx/k/O0OBtpZ73vc6Xjyn0mK+Bbc0TOkYYHF/BgpjCtBBL7ig==", "24395e7d-2078-4924-a630-c36a1d5a8c4b" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "personType",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnits_ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                column: "ResidentialNeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialNeighborhood_NeighborhoodManagerId",
                table: "ResidentialNeighborhood",
                column: "NeighborhoodManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhood_ResidentialNeighborhoodId",
                table: "ResidentialUnits",
                column: "ResidentialNeighborhoodId",
                principalTable: "ResidentialNeighborhood",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentialUnits_ResidentialNeighborhood_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DropTable(
                name: "ResidentialNeighborhood");

            migrationBuilder.DropIndex(
                name: "IX_ResidentialUnits_ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.DropColumn(
                name: "ResidentialNeighborhoodId",
                table: "ResidentialUnits");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5bed72f-ebe0-4156-8af5-5f9b9d863239", "AQAAAAIAAYagAAAAEHxmSInlpDfnb+s5UUkggWAw9JKMBbBvKVoRCdVYRL3a4/l6OioQQVkHg9w9JJW/mw==", "f49b23a8-cb5e-43bd-85cc-cc99e9c62356" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "personType",
                value: 0);
        }
    }
}
