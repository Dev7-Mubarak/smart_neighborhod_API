using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectBlocksFamiliesAndResidentialUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_AspNetUsers_ManagerId",
                table: "ConfilctCases");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_People_ManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_ManagerId",
                table: "Blocks");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "ConfilctCases");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Blocks",
                newName: "BlockManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Budget",
                table: "Projects",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "personType",
                table: "People",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "ResidentialUnitId",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GovernmentInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentInstitutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBlock",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBlock", x => new { x.ProjectId, x.BlockId });
                    table.ForeignKey(
                        name: "FK_ProjectBlock_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectBlock_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialUnits_AspNetUsers_UnitManagerId",
                        column: x => x.UnitManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "UnitManager", "UNITMANAGER" });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "personType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 2,
                column: "personType",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases",
                column: "BlockId");

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
                name: "IX_GovernmentInstitutionContacts_GovernmentInstitutionId",
                table: "GovernmentInstitutionContacts",
                column: "GovernmentInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBlock_BlockId",
                table: "ProjectBlock",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialUnits_UnitManagerId",
                table: "ResidentialUnits",
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
                name: "FK_Blocks_ResidentialUnits_ResidentialUnitId",
                table: "Blocks",
                column: "ResidentialUnitId",
                principalTable: "ResidentialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases",
                column: "BlockId",
                principalTable: "Blocks",
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
                name: "FK_Blocks_ResidentialUnits_ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_Blocks_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropTable(
                name: "GovernmentInstitutionContacts");

            migrationBuilder.DropTable(
                name: "ProjectBlock");

            migrationBuilder.DropTable(
                name: "ResidentialUnits");

            migrationBuilder.DropTable(
                name: "GovernmentInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_ConfilctCases_BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_BlockManagerId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "personType",
                table: "People");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "ConfilctCases");

            migrationBuilder.DropColumn(
                name: "ResidentialUnitId",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "BlockManagerId",
                table: "Blocks",
                newName: "ManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Projects",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "ConfilctCases",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33333333-3333-3333-3333-333333333333",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmationCode", "EmailConfirmationCodeExpiresAt", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", 0, "0a81216d-5342-4974-8f79-39edbeeda934", "admin@example.com", null, null, true, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEG1lXTnNOz040/Vv77tU7Phatd8lTszwJHFjQVPGrHJONmkrUsywdoaxGQ7nDu4YAA==", 1, null, false, "9fe6f59c-8931-44c7-8269-c94ad5fa0360", false, "Admin" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Budget", "Description", "EndDate", "ManagerId", "Name", "ProjectCatogoryId", "ProjectPriority", "ProjectStatus", "StartDate" },
                values: new object[] { 1, 100000m, "هذا مشروع تمهيدي", new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "مشروع تجريبي", 1, 1, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfilctCases_ManagerId",
                table: "ConfilctCases",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ManagerId",
                table: "Blocks",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_ManagerId",
                table: "Blocks",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_AspNetUsers_ManagerId",
                table: "ConfilctCases",
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
