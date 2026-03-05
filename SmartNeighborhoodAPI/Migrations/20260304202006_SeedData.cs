using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c4855af-cd3c-4e41-9ae0-07ef622d81f3", "AQAAAAIAAYagAAAAEGhLi7J2STMrEmmpZyliq7UW+i7LFuXRJJalEeXVy7tiBHnXAGKlj//94vz09FX4qw==", "94b4b68f-8861-4cf7-ba97-1d74435fb789" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88b240e3-b509-4b56-bda7-7046058ea47d", "AQAAAAIAAYagAAAAED/N7MyD098SsvbA0DpsykUroIVT9IOnw335ioluDk6k/w9/muvIWlTfukSjEVDf0g==", "700fabe5-d7ef-44ed-8715-fe7f8ac8b152" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbce729-142c-4cca-b202-8c5490040e7c", "AQAAAAIAAYagAAAAEA4be2NUBMS1+q7TkuGryvm38anYlwFuP6riYA0EmLtyOV1HwGawcl3zvAXgaTVAQw==", "59074c27-6bc1-43e2-865f-3b8830ef6870" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "813161ed-8716-496f-8edb-4ca68c278242", "AQAAAAIAAYagAAAAEHWno+2XfqGuNHdKMWeHShINY1gTOaLyeO4ByL/n6d6P8/nOCGdeXiGWoHhclY911g==", "e7e8c6d9-82dc-44a9-8f37-e9c0bcbc1e20" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d1696d9-94f6-4b4c-abb0-7f7e300f5b5a", "AQAAAAIAAYagAAAAEDiXrMr7GD+bUsWi7dkYWVGlKsB5VSpYk4CqVeXirV3YUN7sDKXb090Sg4FUXDPLfg==", "9cc06796-303b-4a2c-a71d-4390ed41fb4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2afade91-bc41-4fef-ab68-647cfdb42757", "AQAAAAIAAYagAAAAEHksLfDFXSvLVVNnmBfQNKQerqk85clb2lEXzkwGE9U9Y+rlJ0QsOMO9q6RJl7buvA==", "a3f7042d-bcbf-436d-8eaf-2058c999ed9f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5f89e0d-de3e-4b0d-a7d5-b1abb516cc41", "AQAAAAIAAYagAAAAEGgRUES8D6Dx6Jfd4We04swrAwPBTs7x9schV3LyynO4GZIE2mCrTkh9rKhWektTnw==", "86379be0-dfd0-4b3c-b169-bce2de9e4d0f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9167fa2c-3d76-4c9e-b1c3-89a9b6708efc", "AQAAAAIAAYagAAAAEJtMN4xXyzIInE0SWOblpC3i/826lhs4V7+i46uPOLK9Pxcfmfx0PrOcyHoda1JoeA==", "6cf4c11b-76bc-44b5-9e3d-021158d1d1cb" });
        }
    }
}
