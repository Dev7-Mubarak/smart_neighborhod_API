using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    public partial class fixrealtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10df4613-a12a-4502-9159-ade2ce34539f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f22c25ff-eb26-4b22-9a75-0d6de03891fb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "977a6967-1456-468c-9408-2dc5e0dc9057", "67ceb821-8a84-46a8-aac7-22194e485369" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "977a6967-1456-468c-9408-2dc5e0dc9057");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67ceb821-8a84-46a8-aac7-22194e485369");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ThirdName",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30bd6919-93f6-41d3-8692-5ee2c383e166", "5a110e53-3104-465d-920d-82f53f1da054", "Admin", "ADMIN" },
                    { "556118ea-69a3-4a6d-87bc-db6273b0581f", "134a608d-1dff-401d-8e0a-ee5c31cf97a9", "User", "USER" },
                    { "5ddd58bd-223d-43a4-8a8f-f19d77a428f5", "b9f8778a-61f5-40ed-b8b3-bf0a593d7923", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3006ed00-f9c7-4b49-8875-f09e45292368", 0, "becc6510-6777-444c-aae8-49ff714f3d3e", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEFeOOa92fyUWJS9HL4tjM/Svqgi5kzjoNt0LG7Z1XvV8gai7jkoMUt2i+yl06r5m7w==", null, false, "3175793f-1fc9-4bb7-9108-bb5251f41a78", false, "Admin" });

            migrationBuilder.InsertData(
                table: "FamilyCatgories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "A" },
                    { 3, "B" },
                    { 4, "C" }
                });

            migrationBuilder.InsertData(
                table: "FamilyTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Orphans Family" },
                    { 3, "Widow Family" },
                    { 4, "Single Parent Family" },
                    { 5, "Extended Family" }
                });

            migrationBuilder.InsertData(
                table: "MemberTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Father" },
                    { 2, "Mother" },
                    { 3, "Son" },
                    { 4, "Daughter" },
                    { 5, "Grandfather" },
                    { 6, "Grandmother" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "30bd6919-93f6-41d3-8692-5ee2c383e166", "3006ed00-f9c7-4b49-8875-f09e45292368" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "556118ea-69a3-4a6d-87bc-db6273b0581f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ddd58bd-223d-43a4-8a8f-f19d77a428f5");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "30bd6919-93f6-41d3-8692-5ee2c383e166", "3006ed00-f9c7-4b49-8875-f09e45292368" });

            migrationBuilder.DeleteData(
                table: "FamilyCatgories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FamilyCatgories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FamilyCatgories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FamilyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FamilyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FamilyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FamilyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MemberTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30bd6919-93f6-41d3-8692-5ee2c383e166");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3006ed00-f9c7-4b49-8875-f09e45292368");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThirdName",
                table: "People",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10df4613-a12a-4502-9159-ade2ce34539f", "e8ce99bb-d526-4fa4-8d05-3d2ac508d60b", "User", "USER" },
                    { "977a6967-1456-468c-9408-2dc5e0dc9057", "c1d84eeb-ae75-4159-b513-ea8737865a8b", "Admin", "ADMIN" },
                    { "f22c25ff-eb26-4b22-9a75-0d6de03891fb", "5b3256f9-27cb-45ce-8fc7-97b262c49ebe", "BlockManager", "BLOCKMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FamilyMemberId", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67ceb821-8a84-46a8-aac7-22194e485369", 0, "72e4f1a8-d217-4b97-887f-ff4480ac003f", "admin@example.com", true, null, false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAEAACcQAAAAEGS2REt7ij76ABm8zSJmwbLEJSkZ5mz7eCksfVblkwK/rbwD+wesSu4MksThFhnbJA==", null, false, "94e148a7-639e-4214-8a45-192de5827765", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "977a6967-1456-468c-9408-2dc5e0dc9057", "67ceb821-8a84-46a8-aac7-22194e485369" });
        }
    }
}
