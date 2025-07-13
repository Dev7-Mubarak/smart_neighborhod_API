using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddConfilctCaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonComplain");

            migrationBuilder.DropTable(
                name: "Complains");

            migrationBuilder.DropTable(
                name: "ConfilctParties");

            migrationBuilder.DropTable(
                name: "ComplainTypes");

            migrationBuilder.CreateTable(
                name: "ConfilctTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfilctCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPartyId = table.Column<int>(type: "int", nullable: false),
                    SecondPartyId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ConflictTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_ConfilctTypes_ConflictTypeId",
                        column: x => x.ConflictTypeId,
                        principalTable: "ConfilctTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_People_FirstPartyId",
                        column: x => x.FirstPartyId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_People_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ConfilctCases_People_SecondPartyId",
                        column: x => x.SecondPartyId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "275fc87a-d8a3-4640-a795-3f0e8eb99c62", "AQAAAAIAAYagAAAAECtD13HDpQgqjTSDHzwfJ1//Y5fo7+VHKcsr8W1+ybrcGsH5Xux2o2xHzX57/pqW1g==", "e05dba57-e5aa-47db-8626-a609e5aa5cfe" });

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ConflictTypeId",
                table: "ConfilctCases",
                column: "ConflictTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_FirstPartyId",
                table: "ConfilctCases",
                column: "FirstPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_SecondPartyId",
                table: "ConfilctCases",
                column: "SecondPartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfilctCases");

            migrationBuilder.DropTable(
                name: "ConfilctTypes");

            migrationBuilder.CreateTable(
                name: "ComplainTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplainTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfilctParties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfilctParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplainTypeId = table.Column<int>(type: "int", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complains_ComplainTypes_ComplainTypeId",
                        column: x => x.ComplainTypeId,
                        principalTable: "ComplainTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                values: new object[] { "9c4c3d08-b706-48fe-9cfa-ea69c87a65c7", "AQAAAAIAAYagAAAAEEelOrtut6rMF5dDIfCELHkB7Jz4Uw6FvlIJC9Vq4XDshpVwL0ALLpG/UeikEeXyPQ==", "f8294112-0494-4952-bb2e-d38292c3782f" });

            migrationBuilder.CreateIndex(
                name: "IX_Complains_ComplainTypeId",
                table: "Complains",
                column: "ComplainTypeId");

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
