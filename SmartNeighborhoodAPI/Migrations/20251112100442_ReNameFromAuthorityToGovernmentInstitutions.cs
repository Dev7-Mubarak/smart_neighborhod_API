using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class ReNameFromAuthorityToGovernmentInstitutions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Authorities_AuthorityId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Authorities");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AuthorityId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "GovernmentInstitutionId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GovernmentInstitutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentInstitutions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56f289eb-8c87-485d-95f8-a1996a9d093a", "AQAAAAIAAYagAAAAEF2t+TidAOCxOYTxuPgMGkNw4NuaM4/JH2oS4gdBmCCykMFJhEGmn6Ihi2kUBLXNmw==", "6610cdf1-ad8c-413d-b509-6d055b30538f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-bbbb-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c4c5f3b-c0cc-4238-b5a7-91d1a27f3b5b", "AQAAAAIAAYagAAAAEF6MpC3OKjKZhHGYd3pidUcGW8fokAqPuijEyqf+mmilUI1u1eknbDMeD4zBdq32iw==", "aa8c94b1-1dbc-4c56-ad8f-d73554359986" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_GovernmentInstitutionId",
                table: "Contacts",
                column: "GovernmentInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_GovernmentInstitutions_GovernmentInstitutionId",
                table: "Contacts",
                column: "GovernmentInstitutionId",
                principalTable: "GovernmentInstitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_GovernmentInstitutions_GovernmentInstitutionId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "GovernmentInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_GovernmentInstitutionId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "GovernmentInstitutionId",
                table: "Contacts");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Authorities_AuthorityId",
                table: "Contacts",
                column: "AuthorityId",
                principalTable: "Authorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
