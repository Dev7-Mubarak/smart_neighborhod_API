using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartNeighborhoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedDated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84bc576a-6b2f-4f7f-a7e9-459c566a78c3", "AQAAAAIAAYagAAAAEJ47p2HC7pxmTBvtEwaDQpNNU10JK+zwMpCfAKTYEyMrIMyDlUQwX8wCtoRgN6YrCA==", "0c61cbc7-06f5-4c13-942a-d13e45c371be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14ef4f15-c913-45e4-b424-501ded0bc635", "AQAAAAIAAYagAAAAEHkSjmOgFeGjKrPvrEn/W+kq9AK/0Bzj5dCkiLm4wk9G34e8pjfOSOH6KedfFmGwcw==", "d25081fd-4cd4-47dc-a53b-79cb1248a100" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b65d0e5-d512-4d31-b804-dafe8d9a0b3d", "AQAAAAIAAYagAAAAEKJhbIP0QFsfbjDVOMFdJe7wIU56j6U2x5EgCCzS8Jqm+rwg02RPOwqVgJhrMihzjg==", "3e7befa5-253d-4dd5-bf9c-351fe7e5b727" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c1303b0-ba16-407c-968a-767317a6ae5b", "AQAAAAIAAYagAAAAECkJxkGqX9WVE+I01xjSnejAvU0Yr+BmIZJHIpBHu863w0octA+dOFBwFI34KL3nMg==", "36e2421f-5b97-43fd-8395-8211a57d0d87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcaf1b07-41a3-4e24-a207-7369e5609033", "AQAAAAIAAYagAAAAEAVTScppUH+S/pVbxKR5CNe+Vgin5aShMBG5wr03ITgNANSoarOJR6RiHio3kdyNbw==", "6486e10c-5d52-4eca-bf59-59c637b31654" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b924f3ce-53cf-4dbc-987d-af54503bf0d7", "AQAAAAIAAYagAAAAEKAPtJy7VlPVzN9Jm0rTrrb6o8eE2oEfSkF5/Dqd2XIQ3Ndwsms73nz/jr+dByXydA==", "e6af1221-f375-4f9b-aa68-16192bd4a341" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9a0e876-3d39-412b-9bea-4b6ad7930691", "AQAAAAIAAYagAAAAEGe6s9YvM43q8nlN+XRP3/mZ6ECLRFvTwHB0KQoTqcJPhBG1/3pSI4tgsR88z4zUvQ==", "8e455c38-4d59-4f1a-9885-eed8d0433f7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "867dfe28-2659-4019-af6d-31e394a42083", "AQAAAAIAAYagAAAAEIICIv0pZ7gDjzqjzOeTDGF/Z8ttpm1mm5nqi86VDFOu/+2osnrvKclnN4ouqM6Hrw==", "9e2cfa0c-e84a-40a4-91ef-55f222d0a8c2" });

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 2,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 3,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 4,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 5,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 6,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 7,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 8,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 9,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 10,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 11,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 12,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 13,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 14,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 15,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 16,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 17,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 18,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 19,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 20,
                column: "BlockManagerId",
                value: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10101010-1010-1010-1010-101010101010",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae56d344-5f2b-442d-b83a-f34388faea7a", "AQAAAAIAAYagAAAAEFccTjeo4SYJhvWUrehWyjsncLnd+2czL2KH3uF0q0m6chW6c7+sO0KuLy1QTxOZIA==", "ea2fa83e-90f7-43a7-8479-49bb6384e480" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20202020-2020-2020-2020-202020202020",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44def285-5b27-48df-9711-46d0891f0f6e", "AQAAAAIAAYagAAAAEAhQpLMEHdMBcC3ErOahlyGM0ux5B44GjdNuKeCrSE8548BvPc01FXJNjIywvevMZA==", "7a9a8937-e487-4161-8d24-781ed1bdf475" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e13fc60d-8de2-4025-a4f9-15404099d1a2", "AQAAAAIAAYagAAAAEMfw7cDygYmwoOB4eIuO/b4Be1ZIDqtDcEOnPOxsZlJB01MEUO2uN8LoLglPpW5Zpg==", "6f35395d-0ad5-428c-866b-e66a091a5197" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a36dc126-d4a8-481a-93c5-6c1264b49db4", "AQAAAAIAAYagAAAAEONV1etOw0BjCHxd7fsnoQsoYk8UPmQ14M4MUysrL3ps9j5ihvOOLCgZPJgcNlJh9g==", "49aabed9-9f6b-4017-a8a8-56b1394e81cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cccccccc-cccc-cccc-cccc-cccccccccccc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "859863f6-7e6d-48ac-b8b6-558c3e1c6f50", "AQAAAAIAAYagAAAAEILEu5NRzIFl+ZkrDfFEx2OvkLmXkj7F4TyeBY2+6ZkuyxchPod+rCalmQtSqsqZQA==", "7e34b86f-ee7d-40fc-8bde-33031528f477" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dddddddd-dddd-dddd-dddd-dddddddddddd",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "769b8975-4682-49f3-a3fe-93426751d90f", "AQAAAAIAAYagAAAAEC15xOowdoV7X4bRlgD3D8lH7V4av2z1k+ky5OhmIJK32V9AmsaffTwe1HsnhBefQQ==", "febccb30-bee7-49ad-af7b-c16b5de5a358" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3597713-2a02-4875-b4d2-0a12cd59cd43", "AQAAAAIAAYagAAAAEGBzeWAPChvaWxgKbkHfApTDRByM00L4GSVuRFv+uO9/+xzfgT61Yv60Dlsw43G/mQ==", "b99cbe23-0e66-4039-a21d-6194c8712e95" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffffffff-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f390ea2-f797-4560-acff-aad9b364bb10", "AQAAAAIAAYagAAAAEFNo08W31vw68pwIX82PeNbP4L2UrBhN3khMWMxDnJfLHQjThEw13OIWGF0B1f7MUA==", "8414c430-a991-4797-acf4-57944192feeb" });

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 1,
                column: "BlockManagerId",
                value: "dddddddd-dddd-dddd-dddd-dddddddddddd");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 2,
                column: "BlockManagerId",
                value: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 3,
                column: "BlockManagerId",
                value: "ffffffff-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 4,
                column: "BlockManagerId",
                value: "10101010-1010-1010-1010-101010101010");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 5,
                column: "BlockManagerId",
                value: "20202020-2020-2020-2020-202020202020");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 6,
                column: "BlockManagerId",
                value: "dddddddd-dddd-dddd-dddd-dddddddddddd");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 7,
                column: "BlockManagerId",
                value: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 8,
                column: "BlockManagerId",
                value: "ffffffff-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 9,
                column: "BlockManagerId",
                value: "10101010-1010-1010-1010-101010101010");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 10,
                column: "BlockManagerId",
                value: "20202020-2020-2020-2020-202020202020");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 11,
                column: "BlockManagerId",
                value: "dddddddd-dddd-dddd-dddd-dddddddddddd");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 12,
                column: "BlockManagerId",
                value: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 13,
                column: "BlockManagerId",
                value: "ffffffff-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 14,
                column: "BlockManagerId",
                value: "10101010-1010-1010-1010-101010101010");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 15,
                column: "BlockManagerId",
                value: "20202020-2020-2020-2020-202020202020");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 16,
                column: "BlockManagerId",
                value: "dddddddd-dddd-dddd-dddd-dddddddddddd");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 17,
                column: "BlockManagerId",
                value: "eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 18,
                column: "BlockManagerId",
                value: "ffffffff-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 19,
                column: "BlockManagerId",
                value: "10101010-1010-1010-1010-101010101010");

            migrationBuilder.UpdateData(
                table: "Blocks",
                keyColumn: "Id",
                keyValue: 20,
                column: "BlockManagerId",
                value: "20202020-2020-2020-2020-202020202020");
        }
    }
}
