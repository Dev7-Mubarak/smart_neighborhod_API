using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class AddHousingTypeEnumToFamilyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HousingType",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
   
            migrationBuilder.DropColumn(
                name: "HousingType",
                table: "Families");

        }
    }
}
