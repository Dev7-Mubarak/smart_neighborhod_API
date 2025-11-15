using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorityAndContactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorityId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Authorities_AuthorityId",
                        column: x => x.AuthorityId,
                        principalTable: "Authorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88935086-6d78-4556-8ecc-bd5069833117", "AQAAAAIAAYagAAAAEKmm3lQiuh+EOyqY+fdCH215s13tUq8rbVMe+ybkHCeGAGC4ZZIVwc9zfs7pacsnDA==", "d8fa820e-f4f7-4661-8cfb-bb0bc4355e88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f1257d9-da46-4623-a775-9cf40e06637a", "AQAAAAIAAYagAAAAEKFvgCy5GgPwz79HdLKwJZUJ7lW0VpPJPCHuGcFq/sb6+xsfoKeOtC8+pHMTyRDG7A==", "b43489fe-0c8e-428f-a5eb-e269d55c2871" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AuthorityId",
                table: "Contacts",
                column: "AuthorityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Authorities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93d414b2-2477-4865-b63f-079cc5667102", "AQAAAAIAAYagAAAAEEcbxv0gU49pdaP6+AFL1mD2h1xndEKfde7nCcpGvsu8VM1MBCYOjusUPhSgo4iP9Q==", "925ccef5-7127-4391-8374-250fff59e9d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2f83755-0ff0-475e-b40d-581d68d05535", "AQAAAAIAAYagAAAAEOLbob1ZcQrhaad2lSLaWMtaSlSvqZk0GZ+fbCMnlUEmlDupfRSPayzd5gDM2osp/Q==", "a1f06bba-5f1a-4ca1-958a-51f612b8c162" });
        }
    }
}
