using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditTeamsProjectTeamMembersTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFamilies_Blocks_BlockId",
                table: "ProjectFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFamilies_Families_FamilyId",
                table: "ProjectFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFamilies_Projects_ProjectId",
                table: "ProjectFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Projects_ProjectId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ProjectId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFamilies_BlockId",
                table: "ProjectFamilies");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "ProjectFamilies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectFamilies");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectFamilies",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "ProjectFamilies",
                newName: "FamilyID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFamilies_ProjectId",
                table: "ProjectFamilies",
                newName: "IX_ProjectFamilies_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFamilies_FamilyId",
                table: "ProjectFamilies",
                newName: "IX_ProjectFamilies_FamilyID");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "ProjectFamilies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FamilyID",
                table: "ProjectFamilies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTeam_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9cf843-d3cc-40d1-883a-a897a680880a", "AQAAAAIAAYagAAAAEP419igrilcXpjdqEhJfTOs4iBjACXgB3LtAEB5FNXcvEMiq+CimvK2DhbTAV/lv9g==", "94a4fc1b-cd72-43aa-89e7-0c359afa7162" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_ProjectId",
                table: "ProjectTeam",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTeam_TeamId",
                table: "ProjectTeam",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFamilies_Families_FamilyID",
                table: "ProjectFamilies",
                column: "FamilyID",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFamilies_Projects_ProjectID",
                table: "ProjectFamilies",
                column: "ProjectID",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFamilies_Families_FamilyID",
                table: "ProjectFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFamilies_Projects_ProjectID",
                table: "ProjectFamilies");

            migrationBuilder.DropTable(
                name: "ProjectTeam");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "ProjectFamilies",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "FamilyID",
                table: "ProjectFamilies",
                newName: "FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFamilies_ProjectID",
                table: "ProjectFamilies",
                newName: "IX_ProjectFamilies_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFamilies_FamilyID",
                table: "ProjectFamilies",
                newName: "IX_ProjectFamilies_FamilyId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectFamilies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "ProjectFamilies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "ProjectFamilies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectFamilies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1750a3c0-a1f0-4331-acca-41ae98d2e743", "AQAAAAIAAYagAAAAEIKFp2z2kUHjXhpv/yG3l0q/b81uBrmYnIHwoUQoHoVvb21M1RRG9hcFIENyL2jBHA==", "0d89e63f-432f-4631-ba69-fe80de98505c" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProjectId",
                table: "Teams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFamilies_BlockId",
                table: "ProjectFamilies",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFamilies_Blocks_BlockId",
                table: "ProjectFamilies",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFamilies_Families_FamilyId",
                table: "ProjectFamilies",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFamilies_Projects_ProjectId",
                table: "ProjectFamilies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Projects_ProjectId",
                table: "Teams",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
