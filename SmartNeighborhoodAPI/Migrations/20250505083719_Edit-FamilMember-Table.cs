using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class EditFamilMemberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Families_FamilyId",
                table: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FamilyMemberId",
                table: "AspNetUsers");


            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FamilyMembers");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "FamilyMembers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FamilyMemberId",
                table: "AspNetUsers",
                column: "FamilyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Families_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Families_FamilyId",
                table: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FamilyMemberId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "FamilyMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FamilyMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FamilyMemberId",
                table: "AspNetUsers",
                column: "FamilyMemberId",
                unique: true,
                filter: "[FamilyMemberId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Families_FamilyId",
                table: "FamilyMembers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
