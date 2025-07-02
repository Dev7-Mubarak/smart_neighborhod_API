using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class intail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4423cb8-80bc-46d2-99ca-b02250cad153", "AQAAAAIAAYagAAAAECXuvAViSbhMEtldvUj16EgjVL0QRg6BDyhw7MiqUIHHhnrVV5hy9PcZauvmDiAIOQ==", "f4d6797c-e67c-4f36-bafe-dacec960aa83" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "165e8ffa-cccc-4ed8-92b7-94b2dc1ea589", "AQAAAAIAAYagAAAAEMD5L5LkRdI9vB+zVujIFMnS/vB4jWLdciUz0RSaQGKkcpmAmdXI+hEOsRxd4qneYA==", "96e9d731-24c0-469c-a11e-351382e592f9" });
        }
    }
}
