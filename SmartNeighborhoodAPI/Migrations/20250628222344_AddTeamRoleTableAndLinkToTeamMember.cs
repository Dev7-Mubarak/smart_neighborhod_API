using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamRoleTableAndLinkToTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Projects_ProjectId",
                table: "ProjectTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeam_Teams_TeamId",
                table: "ProjectTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam");

            migrationBuilder.DropColumn(
                name: "MemberJob",
                table: "TeamMembers");

            migrationBuilder.RenameTable(
                name: "ProjectTeam",
                newName: "ProjectTeams");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeam_TeamId",
                table: "ProjectTeams",
                newName: "IX_ProjectTeams_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeam_ProjectId",
                table: "ProjectTeams",
                newName: "IX_ProjectTeams_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "TeamRoleId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTeams",
                table: "ProjectTeams",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeamRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a247399f-9e02-4834-8441-4765c327379a", "AQAAAAIAAYagAAAAEE9pHUAGvd8ZmadbsbrDOfPrR5xqW9sk5exLiJpiCVdI54Rsdt/RoYx9dyplqfhREg==", "0ca97a27-13d6-4ade-b87a-7ee807492f74" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamRoleId",
                table: "TeamMembers",
                column: "TeamRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeams_Projects_ProjectId",
                table: "ProjectTeams",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeams_Teams_TeamId",
                table: "ProjectTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_TeamRoles_TeamRoleId",
                table: "TeamMembers",
                column: "TeamRoleId",
                principalTable: "TeamRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeams_Projects_ProjectId",
                table: "ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeams_Teams_TeamId",
                table: "ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_TeamRoles_TeamRoleId",
                table: "TeamMembers");

            migrationBuilder.DropTable(
                name: "TeamRoles");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_TeamRoleId",
                table: "TeamMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTeams",
                table: "ProjectTeams");

            migrationBuilder.DropColumn(
                name: "TeamRoleId",
                table: "TeamMembers");

            migrationBuilder.RenameTable(
                name: "ProjectTeams",
                newName: "ProjectTeam");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeams_TeamId",
                table: "ProjectTeam",
                newName: "IX_ProjectTeam_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeams_ProjectId",
                table: "ProjectTeam",
                newName: "IX_ProjectTeam_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "MemberJob",
                table: "TeamMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTeam",
                table: "ProjectTeam",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9cf843-d3cc-40d1-883a-a897a680880a", "AQAAAAIAAYagAAAAEP419igrilcXpjdqEhJfTOs4iBjACXgB3LtAEB5FNXcvEMiq+CimvK2DhbTAV/lv9g==", "94a4fc1b-cd72-43aa-89e7-0c359afa7162" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_Projects_ProjectId",
                table: "ProjectTeam",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeam_Teams_TeamId",
                table: "ProjectTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
