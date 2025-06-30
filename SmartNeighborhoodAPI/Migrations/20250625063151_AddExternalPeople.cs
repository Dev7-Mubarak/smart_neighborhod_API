using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalPeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplainId",
                table: "ConfilctParties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalPersonId",
                table: "ConfilctParties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalPeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalPeople", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf03eb7c-d7ee-4fd2-844e-bd60ab4ab814", "AQAAAAIAAYagAAAAEDOPyRppJdxM7W5kQOaL++vyzqH2ASENKb29zhEAcdB8MexfJfeEQZ263/cPUES6TQ==", "b00439fb-879a-4d41-bc4f-f98924a62159" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctParties_ComplainId",
                table: "ConfilctParties",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctParties_ExternalPersonId",
                table: "ConfilctParties",
                column: "ExternalPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctParties_Complains_ComplainId",
                table: "ConfilctParties",
                column: "ComplainId",
                principalTable: "Complains",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctParties_ExternalPeople_ExternalPersonId",
                table: "ConfilctParties",
                column: "ExternalPersonId",
                principalTable: "ExternalPeople",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctParties_Complains_ComplainId",
                table: "ConfilctParties");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctParties_ExternalPeople_ExternalPersonId",
                table: "ConfilctParties");

            migrationBuilder.DropTable(
                name: "ExternalPeople");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctParties_ComplainId",
                table: "ConfilctParties");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctParties_ExternalPersonId",
                table: "ConfilctParties");

            migrationBuilder.DropColumn(
                name: "ComplainId",
                table: "ConfilctParties");

            migrationBuilder.DropColumn(
                name: "ExternalPersonId",
                table: "ConfilctParties");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1750a3c0-a1f0-4331-acca-41ae98d2e743", "AQAAAAIAAYagAAAAEIKFp2z2kUHjXhpv/yG3l0q/b81uBrmYnIHwoUQoHoVvb21M1RRG9hcFIENyL2jBHA==", "0d89e63f-432f-4631-ba69-fe80de98505c" });
        }
    }
}
