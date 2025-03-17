using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddTeamAndTeamMemberTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Projects_ProjectId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_People_PersonId",
                table: "TeamMember");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMember_Team_TeamId",
                table: "TeamMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMember",
                table: "TeamMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "494089ff-5ac1-4814-ab52-745fcf2a68ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ab68f23-4709-44db-a3ec-946237e6f449");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "69bd8689-bc7d-49f8-9646-46aa525a7eb5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f17aeeef-ac3b-48f1-9d4f-0975ec11259f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69bd8689-bc7d-49f8-9646-46aa525a7eb5");

            migrationBuilder.RenameTable(
                name: "TeamMember",
                newName: "TeamMembers");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMember_TeamId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMember_PersonId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_ProjectId",
                table: "Teams",
                newName: "IX_Teams_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28130dbc-edbe-4cfe-a1d5-cd86fd5188b4", "06b4f267-091a-4879-9e46-96013035c1ea", "User", "USER" },
                    { "a1a79a25-b7f9-43be-9d73-47524b7f1aee", "23dddfb5-2793-4c0e-bc76-0830cbdad807", "BlockManager", "BLOCKMANAGER" },
                    { "fede070c-3a65-4ccb-8cd1-a030b60b03e4", "6a8342f9-966f-40bd-80e5-85bd4774a9ff", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "21db0f8c-9303-4530-8df6-ef5e00de834a", 0, "702d5b2e-477f-4411-849b-bf2889bea549", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEEihHlp/P1suuKNDDGZKbYC3VodHnvu9ep55DcD2xQmTYfJ4mDufKh9Zslb2e7SQLQ==", null, false, "097653b8-0c61-46fa-aea4-30855a709687", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fede070c-3a65-4ccb-8cd1-a030b60b03e4", "21db0f8c-9303-4530-8df6-ef5e00de834a" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_People_PersonId",
                table: "TeamMembers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Projects_ProjectId",
                table: "Teams",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_People_PersonId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Projects_ProjectId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28130dbc-edbe-4cfe-a1d5-cd86fd5188b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1a79a25-b7f9-43be-9d73-47524b7f1aee");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fede070c-3a65-4ccb-8cd1-a030b60b03e4", "21db0f8c-9303-4530-8df6-ef5e00de834a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fede070c-3a65-4ccb-8cd1-a030b60b03e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21db0f8c-9303-4530-8df6-ef5e00de834a");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameTable(
                name: "TeamMembers",
                newName: "TeamMember");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_ProjectId",
                table: "Team",
                newName: "IX_Team_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMember",
                newName: "IX_TeamMember_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_PersonId",
                table: "TeamMember",
                newName: "IX_TeamMember_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMember",
                table: "TeamMember",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "494089ff-5ac1-4814-ab52-745fcf2a68ee", "2274856c-39d7-44b9-bba2-531953cd6941", "BlockManager", "BLOCKMANAGER" },
                    { "9ab68f23-4709-44db-a3ec-946237e6f449", "9d1e9841-ad15-4f1b-b3ca-895e8df6b694", "User", "USER" },
                    { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "6a636be7-16b3-4936-9ba0-4bcc5fa89396", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "69bd8689-bc7d-49f8-9646-46aa525a7eb5", 0, "21561e88-1986-49c0-acb7-3ab0dadaa87c", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAENfvcsxqibuUtlWpZVnu//feu1MSzXlqwsQMsF0F55Wzu4WuahITRNnrpPZInnWUqA==", null, false, "484c45bd-4e5d-42f8-8079-205d97a9d863", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f17aeeef-ac3b-48f1-9d4f-0975ec11259f", "69bd8689-bc7d-49f8-9646-46aa525a7eb5" });

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Projects_ProjectId",
                table: "Team",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_People_PersonId",
                table: "TeamMember",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMember_Team_TeamId",
                table: "TeamMember",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
