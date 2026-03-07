using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIssueBlockFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlockId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a830ec78-a8fa-4134-9668-e9becd84539c", "AQAAAAIAAYagAAAAEH6K8zlWwaDzErNlXfhhJcjphD78GRVjTXOuekdAEqilJ9VFSa7vP4M7dWk1Zh81kw==", "32c2f0c5-413d-4883-be2f-86269bcdcc00" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef1d1b1d-5102-4660-97c9-98701d0c97b5", "AQAAAAIAAYagAAAAEGI3Cn8msWj2kNmIxCDUZWidAYPE34KLZObBd18/ZzyYYnu3uPqJ0JEwrQObHEingw==", "369bda3f-3c7a-44d8-8120-d083b3feb42e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6ec29be-3d21-4120-b71c-5e83fb270a8b", "AQAAAAIAAYagAAAAEGBbeIErt3UwcIvAkPv4gShIVAZ9dLQUYlDuKglCaYCY/EPf7yOx5EgpGk9pIuTjzw==", "4e610c20-3d27-40a6-aaee-02cfabe496f1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45b8a6bb-27e1-457a-aca5-7d60515ea6aa", "AQAAAAIAAYagAAAAEETOju8Xti8hYnFa9lF1dSvXyZiDKHmMpMUiamvBDezWa2DNG/NrdHB89d8Sw8RXnQ==", "b4c7e13c-22df-4dbd-9082-ffaa1818c237" });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_BlockId",
                table: "Issues",
                column: "BlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Blocks_BlockId",
                table: "Issues",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Blocks_BlockId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_BlockId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "BlockId",
                table: "Issues");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c69ce64a-fdf6-46b3-8ac8-c56e55d4da7f", "AQAAAAIAAYagAAAAENAAwXABtrhrACJ28sDmsz0lKcqnTE7mX3HTdaLh0VJtF6s18xAiPcQ6tM+bcYoxSA==", "45cb0cf1-8a00-4bef-ae6f-41003f3bce06" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af779984-567a-4296-a7f2-ca9f659eb893", "AQAAAAIAAYagAAAAEISFPXdEWO+k/mOmXyO7Ekn5dorCsjYaGN+5JJNRJsp0n4z6pcC/issBYtOFo4ee/A==", "8db8ee5b-0756-4ab3-9cd8-e3ad0aebc845" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "907ab802-4595-424d-934c-a472e1df0ba5", "AQAAAAIAAYagAAAAEHtHrWIYYzgLx7HQUpXIuzNVq8hIGuNTRXxp/YJSvJEZczPTOqhUELOwKfp3KL++PA==", "a85f6be5-7eca-4df0-9de0-a6d7a5b58e5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15b75b50-6ab5-4f68-a020-20aee75fb19a", "AQAAAAIAAYagAAAAEJk9QzJfdKy6ReBgka5g6TRU9r9jKpK3goRQ2EmOLjqFcTxu6RLKNPF3xelLMaxjcg==", "e34aa3ef-3aeb-4364-b063-23e8fc71154c" });
        }
    }
}
