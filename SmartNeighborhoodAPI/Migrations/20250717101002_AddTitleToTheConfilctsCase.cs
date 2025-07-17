using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleToTheConfilctsCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ConfilctCases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "567d089a-e929-4b19-8a3d-b9f96dddd6db", "AQAAAAIAAYagAAAAEPWHNqhGu+2YfLCnQxkR/40u62CJXuN2qPWi0+FavwLUBtluCZoZkIO8Xbne3fcaVA==", "fea1b0af-4d8c-4fcf-a253-e9537e33e452" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ConfilctCases");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7669243-5952-4c97-8f6d-71ebb4f9a689", "AQAAAAIAAYagAAAAEIQsLmPyPHxR1/0sNTse03LBCNXkL257P/m88NwUYX5k6VLGmDs+lTpNJIC64AvQuw==", "7f32e214-8c65-4937-9e66-50aeb8eceac0" });
        }
    }
}
