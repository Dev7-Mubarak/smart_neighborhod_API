using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonComplain");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ConfilctParties");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstParty",
                table: "ConfilctParties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "ConfilctParties",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Outcome",
                table: "Complains",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Complains",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Complains",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Complains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f03f41b-9069-4446-b5e0-81d737210dc1", "AQAAAAIAAYagAAAAEJ/3Y7s4ORyKJ6tWg3M6dQEhrfosu8HqedH7WOgKq0J+8dZvqPCzzMfrWR4o9IEKRg==", "a3beaae7-9f7c-4533-a662-c57c3f9c7df5" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctParties_PersonId",
                table: "ConfilctParties",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ManagerId",
                table: "Complains",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complains_People_ManagerId",
                table: "Complains",
                column: "ManagerId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctParties_People_PersonId",
                table: "ConfilctParties",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complains_People_ManagerId",
                table: "Complains");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctParties_People_PersonId",
                table: "ConfilctParties");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctParties_PersonId",
                table: "ConfilctParties");

            migrationBuilder.DropIndex(
                name: "IX_Complains_ManagerId",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "IsFirstParty",
                table: "ConfilctParties");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ConfilctParties");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Complains");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Complains");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ConfilctParties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Outcome",
                table: "Complains",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Complains",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "PersonComplain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainId = table.Column<int>(type: "int", nullable: false),
                    ConfilctPartyId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    PesonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonComplain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonComplain_Complains_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "Complains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonComplain_ConfilctParties_ConfilctPartyId",
                        column: x => x.ConfilctPartyId,
                        principalTable: "ConfilctParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonComplain_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf03eb7c-d7ee-4fd2-844e-bd60ab4ab814", "AQAAAAIAAYagAAAAEDOPyRppJdxM7W5kQOaL++vyzqH2ASENKb29zhEAcdB8MexfJfeEQZ263/cPUES6TQ==", "b00439fb-879a-4d41-bc4f-f98924a62159" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_ComplainId",
                table: "PersonComplain",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_ConfilctPartyId",
                table: "PersonComplain",
                column: "ConfilctPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonComplain_PersonId",
                table: "PersonComplain",
                column: "PersonId");
        }
    }
}
