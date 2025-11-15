using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addgovernmentinstitutioncontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.CreateTable(
                name: "GovernmentInstitutionContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovernmentInstitutionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentInstitutionContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovernmentInstitutionContacts_GovernmentInstitutions_GovernmentInstitutionId",
                        column: x => x.GovernmentInstitutionId,
                        principalTable: "GovernmentInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8e4bd9d-6dc4-4758-be89-f784dc0faaf1", "AQAAAAIAAYagAAAAENlQUdsy7Ddx44YO1NIqkN0ftSwdhtFTm2aJ4Apx6smVNyF5smMpm/dfihM33d8qcQ==", "ea6efa2d-6b4e-4eb7-a4fc-8f30a9097bb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c87c6b57-2155-4b60-b334-a435e0bd0218", "AQAAAAIAAYagAAAAEA9rv6m9OcQcmEogNw2URe9Zp4tyIkERKToz3qfX8SR+vrBwXHzJpUW6WDECNyE2Pw==", "0d7e9c09-304a-4d3b-9aa4-c240cd0ef31c" });

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentInstitutionContacts_GovernmentInstitutionId",
                table: "GovernmentInstitutionContacts",
                column: "GovernmentInstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GovernmentInstitutionContacts");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GovernmentInstitutionId = table.Column<int>(type: "int", nullable: false),
                    AuthorityId = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_GovernmentInstitutions_GovernmentInstitutionId",
                        column: x => x.GovernmentInstitutionId,
                        principalTable: "GovernmentInstitutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56f289eb-8c87-485d-95f8-a1996a9d093a", "AQAAAAIAAYagAAAAEF2t+TidAOCxOYTxuPgMGkNw4NuaM4/JH2oS4gdBmCCykMFJhEGmn6Ihi2kUBLXNmw==", "6610cdf1-ad8c-413d-b509-6d055b30538f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c4c5f3b-c0cc-4238-b5a7-91d1a27f3b5b", "AQAAAAIAAYagAAAAEF6MpC3OKjKZhHGYd3pidUcGW8fokAqPuijEyqf+mmilUI1u1eknbDMeD4zBdq32iw==", "aa8c94b1-1dbc-4c56-ad8f-d73554359986" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GovernmentInstitutionId",
                table: "Contacts",
                column: "GovernmentInstitutionId");
        }
    }
}
