using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliverytablefortetsing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryCaptains",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCaptains", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0521e14a-ae1c-48ce-ad1d-d7dac8158cca", "AQAAAAIAAYagAAAAEBzUHARAzDKU+P94WRtxi24iGtyf8ewokEUgfULHVuPWZ2lmxk2hSQNWgL6BWV1FTQ==", "b98a6484-a895-4660-b1cc-08a211a37977" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryCaptains");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c789bf98-ed08-406d-af2e-2f43ea1fd3c3", "AQAAAAIAAYagAAAAELuWC/mj29YxdmULlhd4ZD+dpL0vninw8evRSkHZ7oJllbGTOKmq3t0JKFOktgS+lw==", "a946d8db-e79d-46eb-886c-e0b077b40380" });
        }
    }
}
