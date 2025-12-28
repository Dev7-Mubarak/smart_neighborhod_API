using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class addResidentialNeighborhoodManagerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[] { "44444444-4444-4444-4444-444444444444", "ResidentialNeighborhoodManager", "RESIDENTIALNEIGHBORHOODMANAGER" }
            );

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d071835c-1011-471f-8733-afb655216d7b", "AQAAAAIAAYagAAAAEA9bHObqqv0kW/T+TUz3XpOGUpZsdSFl4OtYOaK3RkHOXcxypeJQDnbRcwdszxqqYw==", "7dc8a3fe-84c2-4821-abea-fe6f0c721351" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44444444-4444-4444-4444-444444444444"
);
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29e853df-296e-4117-b43a-84a5c4b265a9", "AQAAAAIAAYagAAAAEPAseORnKSJZ9S7QldA07dhf0pPsS62wtOHFruiTIq1eh5/chp8q7JHes7ygoqSNRQ==", "df765d19-ac7d-4479-9ade-09152e9592a7" });
        }
    }
}
