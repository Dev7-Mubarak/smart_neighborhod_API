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

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = 'a830ec78-a8fa-4134-9668-e9becd84539c', PasswordHash = 'AQAAAAIAAYagAAAAEH6K8zlWwaDzErNlXfhhJcjphD78GRVjTXOuekdAEqilJ9VFSa7vP4M7dWk1Zh81kw==', SecurityStamp = '32c2f0c5-413d-4883-be2f-86269bcdcc00' WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = 'ef1d1b1d-5102-4660-97c9-98701d0c97b5', PasswordHash = 'AQAAAAIAAYagAAAAEGI3Cn8msWj2kNmIxCDUZWidAYPE34KLZObBd18/ZzyYYnu3uPqJ0JEwrQObHEingw==', SecurityStamp = '369bda3f-3c7a-44d8-8120-d083b3feb42e' WHERE Id = 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = 'f6ec29be-3d21-4120-b71c-5e83fb270a8b', PasswordHash = 'AQAAAAIAAYagAAAAEGBbeIErt3UwcIvAkPv4gShIVAZ9dLQUYlDuKglCaYCY/EPf7yOx5EgpGk9pIuTjzw==', SecurityStamp = '4e610c20-3d27-40a6-aaee-02cfabe496f1' WHERE Id = 'cccccccc-cccc-cccc-cccc-cccccccccccc';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = '45b8a6bb-27e1-457a-aca5-7d60515ea6aa', PasswordHash = 'AQAAAAIAAYagAAAAEETOju8Xti8hYnFa9lF1dSvXyZiDKHmMpMUiamvBDezWa2DNG/NrdHB89d8Sw8RXnQ==', SecurityStamp = 'b4c7e13c-22df-4dbd-9082-ffaa1818c237' WHERE Id = 'dddddddd-dddd-dddd-dddd-dddddddddddd';");

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

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = 'c69ce64a-fdf6-46b3-8ac8-c56e55d4da7f', PasswordHash = 'AQAAAAIAAYagAAAAENAAwXABtrhrACJ28sDmsz0lKcqnTE7mX3HTdaLh0VJtF6s18xAiPcQ6tM+bcYoxSA==', SecurityStamp = '45cb0cf1-8a00-4bef-ae6f-41003f3bce06' WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = 'af779984-567a-4296-a7f2-ca9f659eb893', PasswordHash = 'AQAAAAIAAYagAAAAEISFPXdEWO+k/mOmXyO7Ekn5dorCsjYaGN+5JJNRJsp0n4z6pcC/issBYtOFo4ee/A==', SecurityStamp = '8db8ee5b-0756-4ab3-9cd8-e3ad0aebc845' WHERE Id = 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = '907ab802-4595-424d-934c-a472e1df0ba5', PasswordHash = 'AQAAAAIAAYagAAAAEHtHrWIYYzgLx7HQUpXIuzNVq8hIGuNTRXxp/YJSvJEZczPTOqhUELOwKfp3KL++PA==', SecurityStamp = 'a85f6be5-7eca-4df0-9de0-a6d7a5b58e5e' WHERE Id = 'cccccccc-cccc-cccc-cccc-cccccccccccc';");

            migrationBuilder.Sql("UPDATE AspNetUsers SET ConcurrencyStamp = '15b75b50-6ab5-4f68-a020-20aee75fb19a', PasswordHash = 'AQAAAAIAAYagAAAAEJk9QzJfdKy6ReBgka5g6TRU9r9jKpK3goRQ2EmOLjqFcTxu6RLKNPF3xelLMaxjcg==', SecurityStamp = 'e34aa3ef-3aeb-4364-b063-23e8fc71154c' WHERE Id = 'dddddddd-dddd-dddd-dddd-dddddddddddd';");
        }
    }
}
