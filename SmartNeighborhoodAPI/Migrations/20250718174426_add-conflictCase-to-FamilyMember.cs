using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addconflictCasetoFamilyMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_FirstPartyId",
                table: "ConfilctCases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_SecondPartyId",
                table: "ConfilctCases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c789bf98-ed08-406d-af2e-2f43ea1fd3c3", "AQAAAAIAAYagAAAAELuWC/mj29YxdmULlhd4ZD+dpL0vninw8evRSkHZ7oJllbGTOKmq3t0JKFOktgS+lw==", "a946d8db-e79d-46eb-886c-e0b077b40380" });

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_FirstPartyId",
                table: "ConfilctCases",
                column: "FirstPartyId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_SecondPartyId",
                table: "ConfilctCases",
                column: "SecondPartyId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_FirstPartyId",
                table: "ConfilctCases");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_SecondPartyId",
                table: "ConfilctCases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "567d089a-e929-4b19-8a3d-b9f96dddd6db", "AQAAAAIAAYagAAAAEPWHNqhGu+2YfLCnQxkR/40u62CJXuN2qPWi0+FavwLUBtluCZoZkIO8Xbne3fcaVA==", "fea1b0af-4d8c-4fcf-a253-e9537e33e452" });

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_FirstPartyId",
                table: "ConfilctCases",
                column: "FirstPartyId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfilctCases_FamilyMembers_SecondPartyId",
                table: "ConfilctCases",
                column: "SecondPartyId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
